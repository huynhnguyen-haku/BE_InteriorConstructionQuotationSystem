using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using SWP391API.DTO;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context = new InteriorConstructionQuotationSystemContext();


        [HttpGet]
        public IActionResult GetListArticle([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTitle = null, [FromQuery] int? articleTypeId = null, [FromQuery] bool sortByDateDescending = true)
        {
            var query = _context.Articles.Include(a => a.ArticleType).Include(a => a.User)
           .AsQueryable();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                query = query.Where(a => a.Title.Contains(searchTitle));
            }

            if (articleTypeId.HasValue)
            {
                query = query.Where(a => a.ArticleTypeId == articleTypeId.Value);
            }

            query = sortByDateDescending
            ? query.OrderByDescending(a => a.CreatedAt)
            : query.OrderBy(a => a.CreatedAt);
            var totalCount = query.Count();

            List<Article>  articles = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            List<ArticleResponse> responses = new List<ArticleResponse>();
            foreach (var article in articles)
            {
                responses.Add(new ArticleResponse(article));
            }
            var output = new { responses, totalCount };  
            return Ok(output);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailArticle(int id)
        {
            Article article = _context.Articles
            .Include(a => a.ArticleType)
            .Include(a => a.User)
            .FirstOrDefault(a => a.ArticleId == id);

            if (article == null)
                return NotFound();
            ArticleResponse articleResponse = new ArticleResponse(article);
            return Ok(articleResponse);
        }
    }
}
