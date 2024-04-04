using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class QuotationTempRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
