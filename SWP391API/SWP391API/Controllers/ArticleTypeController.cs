using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleTypeController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public ArticleTypeController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetArticleTypes()
        {
            var articleTypes = _context.ArticleTypes.ToList();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(articleTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleTypeById(int id)
        {
            var articleType = _context.ArticleTypes.FirstOrDefault(at => at.ArticleTypeId == id);

            if (articleType == null)
                return NotFound();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(articleType);
        }

        [HttpPost]
        public IActionResult AddArticleType(string typeName)
        {
            ArticleType articleType = new ArticleType();
            articleType.ArticleTypeName = typeName;
            _context.ArticleTypes.Add(articleType);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArticleType(int id, string typeName)
        {
            var articleType = _context.ArticleTypes.FirstOrDefault(at => at.ArticleTypeId == id);

            if (articleType == null)
                return NotFound();

            articleType.ArticleTypeName = typeName;
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticleType(int id)
        {
            var articleType = _context.ArticleTypes.FirstOrDefault(at => at.ArticleTypeId == id);

            if (articleType == null)
                return NotFound();

            _context.ArticleTypes.Remove(articleType);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }
    }
}
