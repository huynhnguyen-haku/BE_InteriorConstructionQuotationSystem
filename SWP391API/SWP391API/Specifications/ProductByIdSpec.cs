using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class ProductByIdSpec : Specification<Product>
    {
        public ProductByIdSpec(int id)
        {
            Query
                .Include(p => p.Category)
                .Include(p => p.User)
                .Include(p => p.QuotationTemps)
                .Include(p => p.QuotationDetails)
                .Include(p => p.ProductInProjects)
                .Where(p => p.ProductId == id);
        }
    }
}
