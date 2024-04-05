using SWP391API.Models;

namespace SWP391API.DTO
{
    public class StyleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double? Price { get; set; }
        public string? Description { get; set; }

        public StyleDTO()
        {
        }
        public StyleDTO(Style style)
        {
            Id = style.Id;
            Name = style.Name;
            Price = style.Price;
            Description = style.Description;
        }
    }
}
