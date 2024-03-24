using System.Text.Json.Serialization;

namespace SWP391API.DTO
{
    public class ContractDTO
    {
        public int? QuotationId { get; set; }
        public string ContractStatus { get; set; } = null!;
      
    }
}
