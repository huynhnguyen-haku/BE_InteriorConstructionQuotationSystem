using Ardalis.Specification;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class CompletedProjectByIdSpec : Specification<CompletedProject>
    {
        public CompletedProjectByIdSpec(int completedProjectId, CompletedProjectFilterDTO completedProjectFilterDTO)
        {
            Query
            .Include(x => x.ProductInProjects)
            .ThenInclude(x => x.Product)
            .Search(p => p.ProjectTitle, "%" + completedProjectFilterDTO.ProjectTitle + "%", completedProjectFilterDTO.ProjectTitle != null)
            .Where(p => p.StartDate >= completedProjectFilterDTO.StartDate, completedProjectFilterDTO.StartDate != null)
            .Where(p => p.EndDate <= completedProjectFilterDTO.EndDate, completedProjectFilterDTO.EndDate != null)
            .Where(p => p.ProjectId == completedProjectId);
        }
    }
}
