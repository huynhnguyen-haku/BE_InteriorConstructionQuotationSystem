namespace SWP391API.DTO
{
    public class SubmitQuotationDTO
    {
        public int? StyleId { get; set; }
        public double? Square { get; set; }
        public double? TotalBill { get; set; }
        public List<QuotationDetailDTO> quotationDetailDTOs { get; set; }
    }
}
