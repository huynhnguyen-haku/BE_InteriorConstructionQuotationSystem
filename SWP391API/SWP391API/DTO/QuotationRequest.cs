using Microsoft.AspNetCore.Mvc;

namespace SWP391API.DTO
{
    public class QuotationRequest     {
        public int QuotationId { get; set; }
        public string QuotationStatus { get; set; } = null!;
    }
}
