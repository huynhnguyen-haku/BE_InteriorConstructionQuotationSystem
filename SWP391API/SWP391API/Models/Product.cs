using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductInProjects = new HashSet<ProductInProject>();
            QuotationDetails = new HashSet<QuotationDetail>();
            QuotationTemps = new HashSet<QuotationTemp>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? Status { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ProductInProject> ProductInProjects { get; set; }
        public virtual ICollection<QuotationDetail> QuotationDetails { get; set; }
        public virtual ICollection<QuotationTemp> QuotationTemps { get; set; }
    }
}
