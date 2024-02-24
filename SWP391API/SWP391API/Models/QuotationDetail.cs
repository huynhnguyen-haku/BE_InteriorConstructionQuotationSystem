using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class QuotationDetail
    {
        public QuotationDetail()
        {
            Quotations = new HashSet<Quotation>();
        }

        public int QuotationDId { get; set; }
        public int ProductId { get; set; }
        public int StyleId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Style Style { get; set; } = null!;
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
