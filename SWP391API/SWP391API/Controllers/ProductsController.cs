using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context = new InteriorConstructionQuotationSystemContext();

        [HttpGet]
        public IActionResult GetListProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10,[FromQuery] string? searchName = null, [FromQuery] int? categoryId = null,
        [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null, [FromQuery] bool sortByDateDescending = true)
        {
            var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(p => p.Name.Contains(searchName));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            query = sortByDateDescending
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt);

            var totalCount = query.Count();
            List<Product> products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            List<ProductResponse> responses = new List<ProductResponse>();
            foreach (var product in products)
            {
                responses.Add(new ProductResponse(product));
            }
            var obj = new { responses, totalCount };
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductDetails(int id)
        {
            Product product = _context.Products
            .Include(p => p.Category)
            .Include(p => p.User)
            .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return Ok(new ProductResponse(product));
        }

        [HttpGet("GetListStyle")]
        public IActionResult GetListStyle()
        {
            List<Style> lis = _context.Styles.ToList();
            return Ok(lis);
        }
    }
}
