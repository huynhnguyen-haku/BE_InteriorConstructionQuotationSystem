namespace SWP391API.DTO
{
    public class ProductFilterDTO
    {
        public string? SearchName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool SortByDateDescending { get; set; } = true;
    }
}
