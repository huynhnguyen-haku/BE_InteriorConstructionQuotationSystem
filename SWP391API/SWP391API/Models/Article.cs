using System;
using System.Collections.Generic;

#nullable disable

namespace SWP391API.Models
{
    public partial class Article
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ArticleTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? Status { get; set; }
        public virtual ArticleType ArticleType { get; set; }
        public virtual User User { get; set; }
    }
}
