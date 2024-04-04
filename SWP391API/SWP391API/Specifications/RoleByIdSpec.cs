using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class RoleByIdSpec : Specification<Role>
    {
        public RoleByIdSpec(int id)
        {
            Query.Where(r => r.RoleId == id);
        }
    }
}
