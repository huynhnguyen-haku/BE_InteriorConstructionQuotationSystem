using SWP391API.Models;

namespace SWP391API.DTO
{
    public class QuotationResponse
    {
        public int QuotationId { get; set; }
        public string QuotationStatus { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int? StyleId { get; set; }
        public double? Square { get; set; }
        public double? TotalBill { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
        public double? Witdh { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? TotalConstructionCost { get; set; }
        public double? TotalProductCost { get; set; }
        public int? HomeStyleId { get; set; }
        public int? FloorConstructionId { get; set; }
        public int? WallConstructId { get; set; }
        public int? CeilingConstructId { get; set; }

        public QuotationResponse(Quotation q)
        {
            QuotationId = q.QuotationId;
            QuotationStatus = q.QuotationStatus;
            Square = q.Square;
            StyleId = q.StyleId;
            TotalBill = q.TotalBill;
            Status = q.Status;
            UserId = q.UserId;
            Witdh = q.Witdh;
            Height = q.Height;
            Length = q.Length;
            CreatedAt = q.CreatedAt;
            TotalConstructionCost = q.TotalConstructionCost;
            TotalProductCost = q.TotalProductCost;
            HomeStyleId = q.HomeStyleId;
            FloorConstructionId = q.FloorConstructionId;
            WallConstructId = q.WallConstructId;
            CeilingConstructId = q.CeilingConstructId;

        }
    }
}
