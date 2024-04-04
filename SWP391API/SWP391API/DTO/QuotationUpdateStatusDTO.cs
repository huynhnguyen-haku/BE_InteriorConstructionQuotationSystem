using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class QuotationUpdateStatusDTO
    {
        [Required]
        public int QuotationId { get; set; }

        [Required]
        public string QuotationStatus { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;
    }
}
