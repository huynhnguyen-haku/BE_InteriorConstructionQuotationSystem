using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using SWP391API.DTO;
using SWP391API.Hubs;
using SWP391API.Models;
using SWP391API.Repositories;
using SWP391API.Specifications;
using SWP391API.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SWP391API.Services.Implements
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly Repository<User> _userRepository;
        private readonly Repository<Role> _roleRepository;

        private readonly IEmailService _emailService;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticateService(Repository<User> userRepository, IConfiguration configuration, Repository<Role> roleRepository, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> authorize(UserDTO userDTO)
        {
            var spec = new UserByUsernameAndPasswordSpec(userDTO.Username, userDTO.Password);

            User? user = await _userRepository.FirstOrDefaultAsync(spec);

            if (user == null)
            {
                throw new Exception(ErrorConstants.UserNotFound);
            }

            if (user.Status != true)
            {
                throw new Exception(ErrorConstants.UserNotActive);
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username), //this claim is required for signalR authorization
                new Claim("UserID", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("FullName", user.Fullname),
                new Claim("Role", user.Role.RoleName)
            };

            string token = generateToken(claims);
            return token;
        }

        public int getCurrentUserId()
        {
            string userIdRaw = _httpContextAccessor.HttpContext.User.FindFirst("UserID")?.Value;

            if (userIdRaw == null)
            {
                return -1;
            }

            return int.Parse(userIdRaw);
        }

        public string generateToken(List<Claim> claims)
        {

            string rawKey = _configuration.GetSection("Jwt")["Key"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(rawKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(100),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        public async Task<bool> register(UserRegisterDTO userRegisterDTO)
        {

            var roleSpec = new RoleByIdSpec(userRegisterDTO.RoleId);
            var isFoundRole = await _roleRepository.AnyAsync(roleSpec);

            if (!isFoundRole)
            {
                throw new Exception(ErrorConstants.RoleNotFound);
            }

            var userByUsernameSpec = new UserByUsernameSpec(userRegisterDTO.Username);
            var isFoundUser = await _userRepository.AnyAsync(userByUsernameSpec);
            if (isFoundUser)
            {
                   throw new Exception(ErrorConstants.UsernameAlreadyExists);
            }

            var userByEmailSpec = new UserByEmailSpec(userRegisterDTO.Email);
            isFoundUser = await _userRepository.AnyAsync(userByEmailSpec);
            if (isFoundUser)
            {
                throw new Exception(ErrorConstants.EmailAlreadyExists);
            }

            User user = new User
            {
                Username = userRegisterDTO.Username,
                Password = userRegisterDTO.Password,
                Email = userRegisterDTO.Email,
                Birthdate = userRegisterDTO.Birthdate,
                Fullname = userRegisterDTO.Fullname,
                RoleId = userRegisterDTO.RoleId,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                Status = false // default status is false to wait for email verification
            };

            user = await _userRepository.AddAsync(user);

            var resendTokenDTO = new ResendTokenDTO(user.Email);
            await sendVerifyEmailToken(resendTokenDTO);

            return true;
        }

        public async Task sendVerifyEmailToken(ResendTokenDTO resendTokenDTO)
        {

            var userSpec = new UserByEmailSpec(resendTokenDTO.Email);
            User? user = await _userRepository.FirstOrDefaultAsync(userSpec);

            if (user == null)
            {
                throw new Exception(ErrorConstants.UserNotFound);
            }

            if (user.Status == true)
            {
                throw new Exception(ErrorConstants.UserAlreadyActive);
            }


            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username), //this claim is required for signalR authorization
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserID", user.UserId.ToString()),
                new Claim("FullName", user.Fullname),
                new Claim("Role", user.Role.RoleName),
                new Claim("IsVerifyEmail", "true")
            };

            string token = $"{_configuration["FrontendUrl"]}/verify?token={generateToken(claims)}";

            _emailService.sendTo(user.Email, "Verify Email", $"Please click the link below to verify your email: {token}");
        }

        public async Task<bool> verifyEmailToken(VerifyTokenDTO verifyTokenDTO)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(verifyTokenDTO.Token);

            var isContainVerifyEmailClaim =  jwtToken.Claims.FirstOrDefault(claim =>
            {
                if (claim.Type == "IsVerifyEmail" && claim.Value == "true")
                {
                    return true;
                }
                return false;
            });

            if (isContainVerifyEmailClaim == null)
            {
                throw new Exception(ErrorConstants.InvalidToken);
            }

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                throw new Exception(ErrorConstants.ExpiredToken);
            }

            var userIdStr = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserID")?.Value;
            var userId = -1;

            if (userIdStr == null || !int.TryParse(userIdStr, out userId))
            {
                throw new Exception(ErrorConstants.InvalidToken);
            }

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception(ErrorConstants.UserNotFound);
            }

            user.Status = true; // active user

            await _userRepository.UpdateAsync(user);

            return true;
        }

        public string getCurrentUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string getCurrentUserRole()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst("Role").Value;
        }
    }
}
