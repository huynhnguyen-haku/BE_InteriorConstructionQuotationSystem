using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class UserRequest
    {
        public int UserId { get; set; }
        public int? QuotationId { get; set; }

        public virtual Quotation? Quotation { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
