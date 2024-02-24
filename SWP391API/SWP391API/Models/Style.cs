using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class Style
    {
        public Style()
        {
            CompletedProjects = new HashSet<CompletedProject>();
            QuotationDetails = new HashSet<QuotationDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CompletedProject> CompletedProjects { get; set; }
        public virtual ICollection<QuotationDetail> QuotationDetails { get; set; }
    }
}
