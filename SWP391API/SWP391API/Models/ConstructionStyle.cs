using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWP391API.Models
{
    public partial class ConstructionStyle
    {
        public ConstructionStyle()
        {
            QuotationCeilingConstructs = new HashSet<Quotation>();
            QuotationFloorConstructions = new HashSet<Quotation>();
            QuotationWallConstructs = new HashSet<Quotation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string ConstructionType { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Quotation> QuotationCeilingConstructs { get; set; }
        [JsonIgnore]

        public virtual ICollection<Quotation> QuotationFloorConstructions { get; set; }
        [JsonIgnore]

        public virtual ICollection<Quotation> QuotationWallConstructs { get; set; }
    }
}
