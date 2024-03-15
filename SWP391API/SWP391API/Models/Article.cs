using System;
using System.Collections.Generic;

namespace SWP391API.Models
{
    public partial class Article
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int ArticleTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? Status { get; set; }
        public string? Img { get; set; }

        public virtual ArticleType ArticleType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
