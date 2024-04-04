using SWP391API.Models;
using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class UpdateProductDTO
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
        [Required]
        public bool Status { get; set; }

    }
}
