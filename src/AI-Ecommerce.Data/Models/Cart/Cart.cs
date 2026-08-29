using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Cart
{
    /// <summary>
    /// Persistent shopping cart — one per customer (CustomerId is unique).
    /// Checkout converts the cart's items into a SalesOrder.
    /// </summary>
    public class Cart
    {
        [Key]
        public long CartId { get; set; }

        [Required]
        public long CustomerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public CustomerMaster? Customer { get; set; }
        public List<CartItem>? Items { get; set; }
    }
}