using System;
using System.Collections.Generic;

#nullable disable

namespace SWP391API.Models
{
    public partial class ArticleType
    {
        public ArticleType()
        {
            Articles = new HashSet<Article>();
        }

        public int ArticleTypeId { get; set; }
        public string ArticleTypeName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
