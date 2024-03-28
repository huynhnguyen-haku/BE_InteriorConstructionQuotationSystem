namespace SWP391API.DTO
{
    public class NewProductRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Status { get; set; }
    }
}
