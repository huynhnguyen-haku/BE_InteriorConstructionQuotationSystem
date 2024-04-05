using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class QuotationByStatusAndDateSpec : Specification<Quotation>
    {
        public QuotationByStatusAndDateSpec(string status, int month, int year)
        {
            Query
                .Where(q => q.QuotationStatus == status)
                .Where(q => q.CreatedAt.Month == month && q.CreatedAt.Year == year);
        }
    }
}
