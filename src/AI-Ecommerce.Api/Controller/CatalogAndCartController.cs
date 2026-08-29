using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models.Cart;
using AI_Ecommerce.Data.Models.Inventory;
using AI_Ecommerce.Data.Models.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context) => _context = context;

        /// <summary>
        /// Active, fully-approved ProductMaster rows with live available stock
        /// (sum of ProductStock.Quantity - ReservedQuantity across warehouses).
        /// This is the new-flow storefront source, replacing the legacy Products table.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.ProductMasters
                .Where(p => p.IsActive && p.Approval3At != null)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductCode,
                    p.ProductName,
                    Category = p.Category != null ? p.Category.CategoryName : "",
                    SubCategory = p.SubCategory != null ? p.SubCategory.SubCategoryName : "",
                    Unit = p.Unit != null ? p.Unit.UnitName : "",
                    SellingPrice = p.SellingPrice ?? 0,
                    GSTPercent = p.GSTPercent ?? 0,
                    AvailableQuantity = _context.ProductStocks
                        .Where(s => s.ProductId == p.ProductId)
                        .Sum(s => s.Quantity - s.ReservedQuantity)
                })
                .ToListAsync();
            return Ok(products);
        }
    }

    // ============================================================
    // Cart + checkout. Checkout converts the cart into a SalesOrder,
    // creates a Payment (COD/UPI, kept Pending), deducts ProductStock and
    // records a StockTransaction for every movement.
    // ============================================================

    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context) => _context = context;

        private long GetCustomerId()
        {
            if (User.FindFirst("AccountType")?.Value != "Customer")
                throw new UnauthorizedAccessException("Only customer accounts can use the cart.");
            var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return long.Parse(sub!);
        }

        private async Task<Cart> GetOrCreateCartAsync(long customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId, Items = new List<CartItem>() };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            return cart;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var customerId = GetCustomerId();
            var cart = await _context.Carts
                .Include(c => c.Items)!
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart?.Items == null)
                return Ok(new { items = new List<object>(), total = 0m, count = 0 });

            var items = cart.Items.Select(i => new
            {
                i.CartItemId,
                i.ProductId,
                i.Product!.ProductCode,
                i.Product.ProductName,
                UnitPrice = i.Product.SellingPrice ?? 0,
                i.Quantity,
                LineTotal = (i.Product.SellingPrice ?? 0) * i.Quantity,
                Available = _context.ProductStocks
                    .Where(s => s.ProductId == i.ProductId)
                    .Sum(s => s.Quantity - s.ReservedQuantity)
            }).ToList();

            return Ok(new
            {
                items,
                total = items.Sum(i => i.LineTotal),
                count = items.Sum(i => i.Quantity)
            });
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(AddCartItemRequest request)
        {
            var customerId = GetCustomerId();
            var product = await _context.ProductMasters.FindAsync(request.ProductId);
            if (product == null || !product.IsActive || product.Approval3At == null)
                return BadRequest("Product not available.");
            if (request.Quantity <= 0)
                return BadRequest("Quantity must be positive.");

            var cart = await GetOrCreateCartAsync(customerId);
            var existing = cart.Items!.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existing != null) existing.Quantity += request.Quantity;
            else cart.Items.Add(new CartItem { CartId = cart.CartId, ProductId = request.ProductId, Quantity = request.Quantity });

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Item added to cart." });
        }

        [HttpPut("items/{cartItemId:long}")]
        public async Task<IActionResult> UpdateItem(long cartItemId, UpdateCartItemRequest request)
        {
            var customerId = GetCustomerId();
            var item = await _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.CartItemId == cartItemId && i.Cart!.CustomerId == customerId);
            if (item == null) return NotFound();
            if (request.Quantity <= 0) return BadRequest("Quantity must be positive.");

            item.Quantity = request.Quantity;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Quantity updated." });
        }

        [HttpDelete("items/{cartItemId:long}")]
        public async Task<IActionResult> RemoveItem(long cartItemId)
        {
            var customerId = GetCustomerId();
            var item = await _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.CartItemId == cartItemId && i.Cart!.CustomerId == customerId);
            if (item == null) return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            var customerId = GetCustomerId();
            var cart = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (cart?.Items != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        /// <summary>
        /// Converts the cart into a SalesOrder. Payment method is "COD" or "UPI";
        /// per requirements no real payment is processed — PaymentStatus stays Pending.
        /// Stock is deducted from ProductStock and a StockTransaction row is recorded
        /// per movement, all inside a single DB transaction.
        /// </summary>
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            var customerId = GetCustomerId();
            var customer = await _context.CustomerMasters.FindAsync(customerId);
            if (customer == null) return Unauthorized();
            if (request.PaymentMethod != "COD" && request.PaymentMethod != "UPI")
                return BadRequest("Payment method must be 'COD' or 'UPI'.");

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (cart?.Items == null || cart.Items.Count == 0)
                return BadRequest("Your cart is empty.");

            var productIds = cart.Items.Select(i => i.ProductId).ToList();
            var products = await _context.ProductMasters
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId, p => p);
            var stocks = await _context.ProductStocks
                .Where(s => productIds.Contains(s.ProductId))
                .ToListAsync();

            decimal subTotal = 0;
            var orderItems = new List<SalesOrderItem>();
            foreach (var item in cart.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                    return BadRequest($"Product {item.ProductId} no longer exists.");

                var available = stocks.Where(s => s.ProductId == item.ProductId).Sum(s => s.Quantity - s.ReservedQuantity);
                if (available < item.Quantity)
                    return BadRequest($"Insufficient stock for {product.ProductName}. Available: {available}");

                var unitPrice = product.SellingPrice ?? 0;
                var lineTotal = unitPrice * item.Quantity;
                subTotal += lineTotal;
                orderItems.Add(new SalesOrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = lineTotal,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName
                });
            }

            var tax = subTotal * 0.10m;
            var shipping = subTotal > 100 ? 0 : 10;
            var total = subTotal + tax + shipping;

            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new SalesOrder
                {
                    SalesOrderNo = $"SO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}",
                    CustomerId = customerId,
                    SubTotal = subTotal,
                    TaxAmount = tax,
                    ShippingCost = shipping,
                    TotalAmount = total,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = "Pending",
                    OrderStatus = "Placed",
                    BillingAddress = customer.AddressLine,
                    BillingCity = customer.City,
                    BillingState = customer.State,
                    BillingCountry = customer.Country,
                    BillingPincode = customer.Pincode,
                    ShippingAddress = customer.AddressLine,
                    ShippingCity = customer.City,
                    ShippingState = customer.State,
                    ShippingCountry = customer.Country,
                    ShippingPincode = customer.Pincode,
                    Items = orderItems
                };
                _context.SalesOrders.Add(order);
                await _context.SaveChangesAsync(); // populates order.SalesOrderId

                // Deduct stock + write the stock ledger
                foreach (var item in cart.Items)
                {
                    var remaining = item.Quantity;
                    foreach (var stock in stocks
                        .Where(s => s.ProductId == item.ProductId)
                        .OrderByDescending(s => s.Quantity - s.ReservedQuantity))
                    {
                        if (remaining <= 0) break;
                        var take = (int)Math.Min(remaining, stock.Quantity - stock.ReservedQuantity);
                        if (take <= 0) continue;

                        stock.Quantity -= take;
                        stock.UpdatedDate = DateTime.UtcNow;
                        _context.StockTransactions.Add(new StockTransaction
                        {
                            ProductId = item.ProductId,
                            WarehouseId = stock.WarehouseId,
                            TransactionType = "OUT",
                            ReferenceId = order.SalesOrderId,
                            Quantity = take,
                            TransactionDate = DateTime.UtcNow,
                            Notes = $"Sales order {order.SalesOrderNo}",
                            CreatedBy = customerId
                        });
                        remaining -= take;
                    }
                }

                _context.Payments.Add(new Payment
                {
                    SalesOrderId = order.SalesOrderId,
                    CustomerId = customerId,
                    Amount = total,
                    PaymentMethod = request.PaymentMethod,
                    Status = "Pending",
                    ReferenceNumber = request.PaymentReference
                });

                _context.CartItems.RemoveRange(cart.Items);
                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                return Ok(new
                {
                    order.SalesOrderId,
                    order.SalesOrderNo,
                    order.TotalAmount,
                    order.PaymentMethod,
                    order.PaymentStatus,
                    order.OrderStatus
                });
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }

    public class AddCartItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateCartItemRequest
    {
        public int Quantity { get; set; }
    }

    public class CheckoutRequest
    {
        /// <summary>"COD" or "UPI"</summary>
        public string PaymentMethod { get; set; } = "COD";
        public string? PaymentReference { get; set; }
    }
}