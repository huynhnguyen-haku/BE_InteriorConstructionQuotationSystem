﻿using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class QuotationDetail
    {
        public int QuotationDId { get; set; }
        public int? QuotationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Quotation? Quotation { get; set; }
    }
}
