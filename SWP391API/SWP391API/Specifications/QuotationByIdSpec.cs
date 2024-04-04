using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class QuotationByIdSpec : Specification<Quotation>
    {
        public QuotationByIdSpec(int id)
        {
            Query
                .Include(q => q.User)
                .Include(q => q.QuotationDetails)
                    .ThenInclude(qd => qd.Product)
                .Include(q => q.CeilingConstruct)
                .Include(q => q.FloorConstruction)
                .Include(q => q.HomeStyle)
                .Include(q => q.Style)
                .Include(q => q.WallConstruct)
                .Where(q => q.QuotationId == id);
        }
    }
}
