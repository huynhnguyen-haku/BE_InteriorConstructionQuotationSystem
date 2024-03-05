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
        private readonly InteriorConstructionQuotationSystemContext _context;

        public ArticlesController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetListArticle([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTitle = null, [FromQuery] int? articleTypeId = null, [FromQuery] bool sortByDateDescending = true)
        {
            try
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

                List<Article> articles = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                List<ArticleResponse> responses = new List<ArticleResponse>();
                foreach (var article in articles)
                {
                    responses.Add(new ArticleResponse(article));
                }
                var output = new { responses, totalCount };
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(articleResponse);
        }


        [HttpPost]
        public IActionResult AddArticle(ArticleRequest article)
        {
            Article a = new Article();
            a.ArticleId = 0;
            a.ArticleTypeId = article.ArticleTypeId;
            a.UserId = article.UserId;
            a.Title = article.Title;
            a.Content = article.Content;
            a.Status = article.Status;

            _context.Articles.Add(a);
            _context.SaveChanges();
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateArticle(ArticleRequest article)
        {
            Article a = _context.Articles.FirstOrDefault(a => a.ArticleId == article.ArticleId);

            if (a != null)
            {
                a.ArticleTypeId = article.ArticleTypeId;
                a.UserId = article.UserId;
                a.Title = article.Title;
                a.Content = article.Content;
                a.Status = article.Status;
                _context.Articles.Update(a);
                _context.SaveChanges();
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok();
            }
            else
            {
                return Ok("This Article isn't exist. Try again!");
            }


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.ArticleId == id);

            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok();
            }
            else
            {
                return Ok("This id of Article isn't exist. Try again!");
            }

        }
    }
}
