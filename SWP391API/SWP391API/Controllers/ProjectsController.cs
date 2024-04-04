using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Services;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ICompletedProjectService _completedProjectService;

        public ProjectsController(ICompletedProjectService completedProjectService)
        {
            _completedProjectService = completedProjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListProducts([FromQuery] CompletedProjectFilterDTO completedProjectFilterDTO)
        {
            try
            {
                var completedProjects = await _completedProjectService.getAll(completedProjectFilterDTO);

                return Ok(completedProjects);
            } catch (Exception ex)
            {
                return BadRequest(new ErrorDTO(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectDetails(int id, [FromQuery] CompletedProjectFilterDTO completedProjectFilterDTO)
        {
            try
            {
                var completedProject = await _completedProjectService.getById(id, completedProjectFilterDTO);

                return Ok(completedProject);

            } catch (Exception ex)
            {
                return BadRequest(new ErrorDTO(ex.Message));
            }
        }

    }
}
