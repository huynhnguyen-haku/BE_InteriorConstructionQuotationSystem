namespace SWP391API.DTO
{
    public class QuotationFilterDTO
    {
        public string? FullnameContains { get; set; }
        public int? QuotationStatus { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public double? MinTotalBill { get; set; }

        public double? MaxTotalBill { get; set; }

    }
}
