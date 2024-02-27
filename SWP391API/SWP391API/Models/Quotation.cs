using System;
using System.Collections.Generic;

#nullable disable

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
        public string QuotationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? StyleId { get; set; }
        public double? Square { get; set; }
        public double? TotalBill { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }

        public virtual Style Style { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<QuotationDetail> QuotationDetails { get; set; }
    }
}
