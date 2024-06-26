﻿using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class Style
    {
        public Style()
        {
            CompletedProjects = new HashSet<CompletedProject>();
            Quotations = new HashSet<Quotation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double? Price { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<CompletedProject> CompletedProjects { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
