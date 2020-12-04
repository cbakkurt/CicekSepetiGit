using System;

namespace CicekSepeti.Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        public int Count { get; set; }
    }
}
