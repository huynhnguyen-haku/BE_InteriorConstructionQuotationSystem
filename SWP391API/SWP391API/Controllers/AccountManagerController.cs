using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public AccountManagerController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers(
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 10,
       [FromQuery] string? searchTerm = null)
        {
            try
            {
                var query = _context.Users.AsQueryable();
                searchTerm = searchTerm == null ? "" : searchTerm;
                // Apply search filter


                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(u =>
                        u.Fullname.Contains(searchTerm) ||
                        u.Email.Contains(searchTerm) ||
                        u.Username.Contains(searchTerm));
                }

                var totalCount = query.Count();

                var users = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new { Users = users, TotalCount = totalCount });

            }
            catch (Exception e)
            {
                return BadRequest("Có lỗi xảy ra: " + e.Message);
            }
            finally
            {
                _context.Dispose(); // Giải phóng tài nguyên
            }

        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                    return Ok("This user isn't exist. Try again!");
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDTO addUserDTO)
        {
            var newUser = new User
            {
                Username = addUserDTO.Username,
                Password = addUserDTO.Password,
                Fullname = addUserDTO.Fullname,
                Birthdate = addUserDTO.Birthdate,
                Email = addUserDTO.Email,
                PhoneNumber = addUserDTO.PhoneNumber,
                AvtUrl = addUserDTO.AvtUrl,
                RoleId = addUserDTO.RoleId,
                Status = addUserDTO.Status,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, UpdateUserDTO updateUserDTO)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return Ok("This user isn't exist. Try again!");

            user.Fullname = updateUserDTO.Fullname;
            user.Birthdate = updateUserDTO.Birthdate;
            user.Email = updateUserDTO.Email;
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            user.AvtUrl = updateUserDTO.AvtUrl;
            user.RoleId = updateUserDTO.RoleId;
            user.Status = updateUserDTO.Status;
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return Ok("This user isn't exist. Try again!");

            _context.Users.Remove(user);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }
    }
}
