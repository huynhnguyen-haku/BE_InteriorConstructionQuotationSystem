using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public CategoryController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(category);
        }

        [HttpPost]
        public IActionResult AddCategory(string updatedCategoryName)
        {
            Category c = new Category();
            c.Name = updatedCategoryName;
            _context.Categories.Add(c);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, String updatedCategoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            category.Name = updatedCategoryName;
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }
    }
}
