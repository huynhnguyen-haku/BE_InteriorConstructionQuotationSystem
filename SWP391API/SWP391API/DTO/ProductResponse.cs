using SWP391API.Models;

namespace SWP391API.DTO
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool? Status { get; set; }

        public ProductResponse(Product p)
        {
            ProductId = p.ProductId;
            CategoryId = p.CategoryId;
            UserId = p.UserId;
            Name = p.Name;
            Price = p.Price;
            Description = p.Description;
            Size = p.Size;
            ImageUrl = p.ImageUrl;
            CreatedAt = p.CreatedAt;
            UpdatedAt = p.UpdatedAt;
            CategoryName = p.Category.Name;
            Status = p.Status;
        }
    }
}
