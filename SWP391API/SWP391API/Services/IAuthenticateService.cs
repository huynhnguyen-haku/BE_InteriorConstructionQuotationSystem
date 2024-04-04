using SWP391API.DTO;
using SWP391API.Models;
using System.Security.Claims;

namespace SWP391API.Services
{
    public interface IAuthenticateService
    {
        Task<bool> register(UserRegisterDTO userRegisterDTO);

        Task sendVerifyEmailToken(ResendTokenDTO resendTokenDTO);

        Task<bool> verifyEmailToken(VerifyTokenDTO verifyTokenDTO);

        string generateToken(List<Claim> claims);

        Task<string> authorize(UserDTO userDTO);

        int getCurrentUserId();

        string getCurrentUsername();
    }
}
