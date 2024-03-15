using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class Quotation
    {
        public Quotation()
        {
            Contracts = new HashSet<Contract>();
            QuotationDetails = new HashSet<QuotationDetail>();
        }

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

        public virtual ConstructionStyle? CeilingConstruct { get; set; }
        public virtual ConstructionStyle? FloorConstruction { get; set; }
        public virtual HomeStyle? HomeStyle { get; set; }
        public virtual Style? Style { get; set; }
        public virtual User? User { get; set; }
        public virtual ConstructionStyle? WallConstruct { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<QuotationDetail> QuotationDetails { get; set; }
    }
}
