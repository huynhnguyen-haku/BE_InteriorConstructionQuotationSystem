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
        private readonly InteriorConstructionQuotationSystemContext _context;

        public ProductsController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetListProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10,[FromQuery] string? searchName = null, [FromQuery] int? categoryId = null,
        [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null, [FromQuery] bool sortByDateDescending = true)
        {
            try {
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
                // Mapping từ product sang product response, khi trả về thì sẽ là respone
                List<ProductResponse> responses = new List<ProductResponse>();
                foreach (var product in products)
                {
                    responses.Add(new ProductResponse(product));
                }
                var obj = new { responses, totalCount };
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok(obj);
                }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(new ProductResponse(product));
        }

        [HttpGet("GetListStyle")]
        public IActionResult GetListStyle()
        {
            List<Style> lis = _context.Styles.ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(lis);
        }

        [HttpGet("GetListHomeStyle")]
        public IActionResult GetListHomeStyle()
        {
            var lis = _context.HomeStyles.ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(lis);
        }

        [HttpGet("GetListConstructionStyles")]
        public IActionResult GetConstructionStyles(string TypeID)
        {
            var lis = _context.ConstructionStyles.Where(x=>x.ConstructionType == TypeID).ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(lis);
        }

        [HttpPost]
        public IActionResult AddProduct(NewProductRequest product)
        {
            // Tạo một sản phẩm mới và gán các thuộc tính từ request
            Product p = new Product();
            p.CategoryId = product.CategoryId;
            p.UserId = product.UserId;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Description = product.Description;
            p.Size = product.Size;
            p.ImageUrl = product.ImageUrl;
            p.CreatedAt = DateTime.Now; // Sử dụng thời gian hiện tại nếu không có giá trị được cung cấp
            p.Status = product.Status;

            // Thêm sản phẩm vào context và lưu thay đổi vào database
            _context.Products.Add(p);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductRequest product)
        {
            Product p = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (p == null)
            {
                return Ok("This Product not exist try again!");
             }
            p.CategoryId = product.CategoryId;
            p.UserId = product.UserId;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Description = product.Description;
            p.Size = product.Size;
            p.ImageUrl = product.ImageUrl;
            p.CreatedAt = DateTime.Now;
            p.Status = product.Status;

            _context.Products.Update(p);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok();

            }
            else
            {
                return Ok("This Product not exist try again!");
            }

        }
    }
}
