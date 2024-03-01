using System.Text.Json.Serialization;

namespace SWP391API.DTO
{
    public class ArticleRequest
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int ArticleTypeId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Status { get; set; }
    }
}
