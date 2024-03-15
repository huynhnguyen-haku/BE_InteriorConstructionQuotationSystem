using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class QuotationTemp
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
