using System;
using System.Collections.Generic;

#nullable disable

namespace SWP391API.Models
{
    public partial class CompletedProject
    {
        public CompletedProject()
        {
            ProductInProjects = new HashSet<ProductInProject>();
        }

        public int ProjectId { get; set; }
        public int StyleId { get; set; }
        public int UserId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectImage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Style Style { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductInProject> ProductInProjects { get; set; }
    }
}
