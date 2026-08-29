using System;
using System.ComponentModel.DataAnnotations;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Cart
{
    /// <summary>A product line in a customer's cart (unique per cart + product).</summary>
    public class CartItem
    {
        [Key]
        public long CartItemId { get; set; }

        [Required]
        public long CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public Cart? Cart { get; set; }
        public ProductMaster? Product { get; set; }
    }
}