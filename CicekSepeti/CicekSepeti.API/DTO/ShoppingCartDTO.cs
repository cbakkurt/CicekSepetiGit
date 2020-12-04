using System;
using System.ComponentModel.DataAnnotations;

namespace CicekSepeti.API.DTO
{
    public class ShoppingCartDTO
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        [Range(1, 100, ErrorMessage = "Adet 1 ile 100 arasında olmalıdır.")]
        public int Count { get; set; }
    }
}
