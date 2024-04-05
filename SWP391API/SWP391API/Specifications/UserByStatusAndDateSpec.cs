using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class UserByStatusAndDateSpec : Specification<User>
    {
        public UserByStatusAndDateSpec(bool status, int month, int year)
        {
            Query
                .Where(u => u.Status == status)
                .Where(u => u.CreatedAt.Month == month && u.CreatedAt.Year == year);
        }
    }
}
