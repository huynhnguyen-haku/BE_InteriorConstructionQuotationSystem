using System;
using System.Collections.Generic;

#nullable disable

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
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductInProject> ProductInProjects { get; set; }
        public virtual ICollection<QuotationDetail> QuotationDetails { get; set; }
        public virtual ICollection<QuotationTemp> QuotationTemps { get; set; }
    }
}
