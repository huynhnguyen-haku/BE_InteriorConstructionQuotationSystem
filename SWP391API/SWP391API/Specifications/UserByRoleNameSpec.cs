using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class UserByRoleNameSpec : Specification<User>
    {
        public UserByRoleNameSpec(string roleName)
        {
            Query
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == roleName);
        }
    }
}
