using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Services;
using System.Security.Claims;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;
        private readonly IAuthenticateService _authenticateService;

        public UserController(InteriorConstructionQuotationSystemContext context, IAuthenticateService authenticateService)
        {
            _context = context;
            _authenticateService = authenticateService;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken([FromBody] VerifyTokenDTO verifyTokenDTO)
        {
            try
            {
                bool result = await _authenticateService.verifyEmailToken(verifyTokenDTO);

                if (result)
                {
                    return Ok();
                }

                return BadRequest(new ErrorDTO("Token is invalid"));
            }
            catch(Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            } 
        }

        [HttpPost("Token/resend")]
        public async Task<IActionResult> ResendToken([FromBody] ResendTokenDTO resendTokenDTO)
        {
            try
            {
                await _authenticateService.sendVerifyEmailToken(resendTokenDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                await _authenticateService.register(userRegisterDTO);

                return Ok();
            }catch (Exception e)
            {
                
                return BadRequest(new ErrorDTO(e.Message));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            try
            {
                string token = await _authenticateService.authorize(userDTO);

                return Ok(new { token });

            }catch(Exception ex)
            {
                return BadRequest(new ErrorDTO(ex.Message));
            }

        }

        //get user info by extracting jwt token
        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var username = claim[0].Value;
            var userId = claim[1].Value;
            var fullName = claim[2].Value;
            var avtURL = claim[3].Value;
            var role = claim[4].Value;
            User u = _context.Users.FirstOrDefault(x => x.UserId == int.Parse(userId));
            _context.Dispose(); // Giải phóng tài nguyên

            return Ok(u);
        }

    }
}
