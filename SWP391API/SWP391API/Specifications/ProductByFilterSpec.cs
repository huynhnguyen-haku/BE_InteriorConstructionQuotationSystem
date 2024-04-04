using Ardalis.Specification;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class ProductByFilterSpec : Specification<Product>
    {
        public ProductByFilterSpec(ProductFilterDTO productFilterDTO)
        {
            Query
                .Include(p => p.Category)
                .Include(p => p.User)
                .Search(p => p.Name, "%" + productFilterDTO.SearchName + "%", productFilterDTO.SearchName != null)
                .Where(p => p.Price >= productFilterDTO.MinPrice, productFilterDTO.MinPrice != null)
                .Where(p => p.Price <= productFilterDTO.MaxPrice, productFilterDTO.MaxPrice != null)
                .Where(p => p.CategoryId == productFilterDTO.CategoryId, productFilterDTO.CategoryId != null)
                .OrderByDescending(p => p.CreatedAt, productFilterDTO.SortByDateDescending);
        }
    }
}
