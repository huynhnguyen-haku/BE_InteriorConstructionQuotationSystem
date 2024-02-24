using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class Quotation
    {
        public Quotation()
        {
            Contracts = new HashSet<Contract>();
        }

        public int QuotationId { get; set; }
        public string QuotationStatus { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int? QuotationDId { get; set; }

        public virtual QuotationDetail? QuotationD { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
