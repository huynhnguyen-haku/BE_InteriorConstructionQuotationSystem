using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;
using System.Collections.Generic;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public ProjectsController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetListProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchName = null,[FromQuery] bool sortByDateDescending = true)
        {
            var query = _context.CompletedProjects
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(p => p.ProjectTitle.Contains(searchName));
            }


            query = sortByDateDescending
                ? query.OrderByDescending(p => p.StartDate)
                : query.OrderBy(p => p.StartDate);

            var totalCount = query.Count();
            List<CompletedProject> products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var obj = new { products, totalCount };
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectDetails(int id)
        {
            CompletedProject project = _context.CompletedProjects
            .FirstOrDefault(p => p.ProjectId == id);
            List<ProductInProject> productInProjects = _context.ProductInProjects.Where(x => x.ProjectId == id).ToList();
            List<ProductResponse> pl = new List<ProductResponse>();
            foreach (var product in productInProjects)
            {
                Product pro = _context.Products.Include(x=>x.Category).FirstOrDefault(x => x.ProductId == product.ProductId);
                ProductResponse productResponse = new ProductResponse(pro);
                pl.Add(productResponse);
            }
            if (project == null)
                return NotFound();
            ProjectResponse response = new ProjectResponse(project, pl);
            _context.Dispose(); // Giải phóng tài nguyên
            return Ok(response);
        }

    }
}
