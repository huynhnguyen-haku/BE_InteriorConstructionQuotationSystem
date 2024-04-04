using Ardalis.Specification;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class UserByUsernameAndPasswordSpec : Specification<User>    
    {
        public UserByUsernameAndPasswordSpec(string username, string password)
        {
            Query
                .Include(u => u.Role)
                .Where(u => u.Username == username && 
                            u.Password == password);
        }
    }
}
