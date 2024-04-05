using SWP391API.Models;

namespace SWP391API.DTO
{
    public class QuotationResponseDTO
    {
        public int QuotationId { get; set; }
        public string QuotationStatus { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int? StyleId { get; set; }
        public double? Square { get; set; }
        public double? TotalBill { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
        public string Fullname { get; set; }

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

        public ConstructionStyleDTO CeilingConstruct { get; set; }
        public ConstructionStyleDTO FloorConstruction { get; set; }
        public ConstructionStyleDTO WallConstruct { get; set; }
        public HomeStyleDTO HomeStyle { get; set; }
        public StyleDTO StyleDTO { get; set; }

        public QuotationResponseDTO(Quotation q)
        {
            QuotationId = q.QuotationId;
            QuotationStatus = q.QuotationStatus;
            Square = q.Square;
            StyleId = q.StyleId;
            TotalBill = q.TotalBill;
            Status = q.Status;
            UserId = q.UserId;
            Fullname = q.User.Fullname;
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

            CeilingConstruct = new ConstructionStyleDTO(q.CeilingConstruct);
            FloorConstruction = new ConstructionStyleDTO(q.FloorConstruction);
            WallConstruct = new ConstructionStyleDTO(q.WallConstruct);
            HomeStyle = new HomeStyleDTO(q.HomeStyle);
            StyleDTO = new StyleDTO(q.Style);

            quotationDetailDTOs = q.QuotationDetails.Select(qd => new QuotationDetailDTO(qd)).ToList();

        }
    }
}
