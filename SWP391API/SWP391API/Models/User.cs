﻿using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class User
    {
        public User()
        {
            Articles = new HashSet<Article>();
            CompletedProjects = new HashSet<CompletedProject>();
            Products = new HashSet<Product>();
            QuotationTemps = new HashSet<QuotationTemp>();
            Quotations = new HashSet<Quotation>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvtUrl { get; set; }
        public int RoleId { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<CompletedProject> CompletedProjects { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<QuotationTemp> QuotationTemps { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
