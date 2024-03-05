using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public RoleController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _context.Roles.ToList();
            _context.Dispose(); // Giải phóng tài nguyên

            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public IActionResult GetRole(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);

            if (role == null)
                return NotFound();
            _context.Dispose(); // Giải phóng tài nguyên

            return Ok(role);
        }

        [HttpPost]
        public IActionResult CreateRole(string RoleName)
        {
            var newRole = new Role
            {
                RoleName = RoleName,
            };

            _context.Roles.Add(newRole);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên

            return Ok();
        }

        [HttpPut("{roleId}")]
        public IActionResult UpdateRole(int roleId, string RoleName)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);

            if (role == null)
                return NotFound();

            role.RoleName = RoleName;

            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên

            return Ok();
        }

        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);

            if (role == null)
                return NotFound();

            _context.Roles.Remove(role);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên

            return Ok();
        }
    }
}
