using SWP391API.Models;

namespace SWP391API.DTO
{
    public class HomeStyleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public HomeStyleDTO()
        {
        }

        public HomeStyleDTO(HomeStyle homeStyle)
        {
            Id = homeStyle.Id;
            Name = homeStyle.Name;
            Price = homeStyle.Price;
        }
    }
}
