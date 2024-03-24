using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public DashboardController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }


        [HttpGet("GetNumberProducts")]
        public IActionResult GetNumberProducts()
        {
            int total = _context.Products.ToList().Count;

          
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(total);
        }


        [HttpGet("GetNumberUser")]
        public IActionResult GetNumberUser()
        {
            int total = _context.Users.ToList().Count;


            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(total);
        }


        [HttpGet("GetNumberCategory")]
        public IActionResult GetNumberCategory()
        {
            int total = _context.Categories.ToList().Count;


            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(total);
        }


        [HttpGet("GetNumberContract")]
        public IActionResult GetNumberContract()
        {
            int total = _context.Quotations.ToList().Count;
            total += _context.Contracts.ToList().Count;

            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(total);
        }
    }
}
