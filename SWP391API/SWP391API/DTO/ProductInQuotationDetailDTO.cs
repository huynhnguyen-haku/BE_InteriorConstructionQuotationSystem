using SWP391API.Models;

namespace SWP391API.DTO
{
    public class ProductInQuotationDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public ProductInQuotationDetailDTO()
        {
        }

        public ProductInQuotationDetailDTO(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.Name;
            ProductImage = product.ImageUrl;
            Price = product.Price;
        }
    }
}
