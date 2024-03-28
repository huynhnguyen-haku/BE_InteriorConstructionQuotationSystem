using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWP391API.Models
{
    public partial class HomeStyle
    {
        public HomeStyle()
        {
            Quotations = new HashSet<Quotation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
