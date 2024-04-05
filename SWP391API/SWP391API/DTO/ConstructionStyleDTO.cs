using SWP391API.Models;

namespace SWP391API.DTO
{
    public class ConstructionStyleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ConstructionType { get; set; }

        public ConstructionStyleDTO()
        {
        }

        public ConstructionStyleDTO(ConstructionStyle constructionStyle)
        {
            Id = constructionStyle.Id;
            Name = constructionStyle.Name;
            Price = constructionStyle.Price;
            ConstructionType = constructionStyle.ConstructionType;
        }
    }
}
