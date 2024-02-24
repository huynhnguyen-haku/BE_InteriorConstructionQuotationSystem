using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class ProductInProject
    {
        public int ProductId { get; set; }
        public int ProjectId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual CompletedProject Project { get; set; } = null!;
    }
}
