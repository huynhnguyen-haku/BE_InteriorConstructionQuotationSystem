using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class UserByEmailSpec : Specification<User>
    {
        public UserByEmailSpec(string email)
        {
            Query
                .Include(u => u.Role)
                .Where(u => u.Email == email);
        }
    }
}
