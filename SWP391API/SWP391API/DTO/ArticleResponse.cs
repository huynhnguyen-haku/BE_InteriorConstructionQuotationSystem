using SWP391API.Models;

namespace SWP391API.DTO
{
    public class ArticleResponse
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int ArticleTypeId { get; set; }
        public string Img { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ArticleTypeName { get; set; }
        public string? AuthorName { get; set; }
        public bool? Status { get; set; }
        public ArticleResponse(Article a)
        {
            ArticleId = a.ArticleId;
            UserId = a.UserId;
            Title = a.Title;
            Content = a.Content;
            ArticleTypeId = a.ArticleTypeId;
            CreatedAt = a.CreatedAt;
            ArticleTypeName = a.ArticleType.ArticleTypeName;
            AuthorName = a.User.Username;
            Status = a.Status;
            Img = a.Img;
        }
    }
}
