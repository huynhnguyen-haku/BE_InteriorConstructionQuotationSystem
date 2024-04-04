using SWP391API.Models;
using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class QuotationDetailDTO
    {
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public QuotationDetailDTO(QuotationDetail qd)
        {
            ProductId = qd.ProductId;
            Quantity = qd.Quantity;
            Price = qd.Price;
        }

        public QuotationDetailDTO()
        {
        }

    }
}

