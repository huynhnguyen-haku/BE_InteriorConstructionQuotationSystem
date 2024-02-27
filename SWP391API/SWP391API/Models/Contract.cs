using System;
using System.Collections.Generic;

#nullable disable

namespace SWP391API.Models
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public int? QuotationId { get; set; }
        public string ContractStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Quotation Quotation { get; set; }
    }
}
