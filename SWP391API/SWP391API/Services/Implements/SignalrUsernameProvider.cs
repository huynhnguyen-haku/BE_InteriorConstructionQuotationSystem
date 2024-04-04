using System.Security.Claims;

namespace SWP391API.Services.Implements
{
    public class SignalrUsernameProvider
    {
        public virtual string GetUsername(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
