using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class SubmitQuotationDetailDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        public SubmitQuotationDetailDTO()
        {
        }


    }
}
