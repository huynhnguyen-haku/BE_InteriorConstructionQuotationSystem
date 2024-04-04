using SWP391API.Models;

namespace SWP391API.DTO
{
    public class ProductInProjectDTO
    {
        public int ProductId { get; set; }
        public int ProjectId { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Status { get; set; }

        public ProductInProjectDTO(ProductInProject productInProject)
        {
            ProductId = productInProject.ProductId;
            ProjectId = productInProject.ProjectId;
            Quantity = (int) productInProject.Quantity;
            CategoryId = productInProject.Product.CategoryId;
            UserId = productInProject.Product.UserId;
            Name = productInProject.Product.Name;
            Price = productInProject.Product.Price;
            Description = productInProject.Product.Description;
            Size = productInProject.Product.Size;
            ImageUrl = productInProject.Product.ImageUrl;
            CreatedAt = productInProject.Product.CreatedAt.Value;
            UpdatedAt = productInProject.Product.UpdatedAt.Value;
            Status = productInProject.Product.Status.Value;
        }

        public ProductInProjectDTO()
        {
        }
    }
}
