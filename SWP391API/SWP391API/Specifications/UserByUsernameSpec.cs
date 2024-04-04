using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class UserByUsernameSpec : Specification<User>
    {
        public UserByUsernameSpec(string username)
        {
            Query
                .Include(u => u.Role)
                .Where(u => u.Username == username);
        }
    }
}
