namespace SWP391API.DTO
{
    public class GraphMonthReportDTO
    {
        public int PendingQuotationCount { get; set; }
        public int DoneQuotationCount { get; set; }
        public int CancelledQuotationCount { get; set; }
        public int TotalQuotationCount { get; set;}

        public int ActivatedUserCount { get; set; }
        public int DeactivatedUserCount { get; set; }
        public int TotalUserCount { get; set; }
    }
}
