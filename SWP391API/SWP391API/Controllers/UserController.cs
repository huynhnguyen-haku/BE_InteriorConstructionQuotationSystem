﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SWP391API.DTO;
using SWP391API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context = new InteriorConstructionQuotationSystemContext();

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO userdto)
        {
            User user = new User();
            user.Username = userdto.Username;
            user.Password = userdto.Password;
            user.Email = userdto.Email;
            user.Birthdate = userdto.Birthdate;
            user.Fullname = userdto.Fullname;
            user.RoleId = userdto.RoleId;
            user.PhoneNumber = userdto.PhoneNumber;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new { message = "Register successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login(UserDTO userDTO)
        {
            var user = _context.Users.Include(x=>x.Role).SingleOrDefault(x => x.Username == userDTO.Username && x.Password == userDTO.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(new { token = CreateToken(user) });
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim("UserID", user.UserId.ToString()));
            claims.Add(new Claim("Role", user.Role.RoleName));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SupersuperSecretKeyabv@@vvx1231321x4xxx0bd001563085fc35165329ea1ff5c5ecbdbbeef"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            user.Token = jwt.ToString();
            user.ExpireDate = DateTime.Now.AddMinutes(30);
            _context.Update(user);
            _context.SaveChanges();
            return jwt;
        }
        

    }
}