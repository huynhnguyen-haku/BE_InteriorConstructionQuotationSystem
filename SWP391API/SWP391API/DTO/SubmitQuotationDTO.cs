namespace SWP391API.DTO
{
    public class SubmitQuotationDTO
    {
        public int? StyleId { get; set; }
        public double? Witdh { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? TotalConstructionCost { get; set; }
        public double? TotalProductCost { get; set; }
        public int? HomeStyleId { get; set; }
        public int? FloorConstructionId { get; set; }
        public int? WallConstructId { get; set; }
        public int? CeilingConstructId { get; set; }
        public List<QuotationDetailDTO> quotationDetailDTOs { get; set; }
    }
}
