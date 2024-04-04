using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class CreateProductDTO
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = null!;


        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Size { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
