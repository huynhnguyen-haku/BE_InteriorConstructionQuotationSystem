using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Repositories;
using SWP391API.Specifications;
using SWP391API.Utilities;

namespace SWP391API.Services.Implements
{
    public class CompletedProjectService : ICompletedProjectService
    {
        private readonly Repository<CompletedProject> _completedProjectRepository;

        public CompletedProjectService(Repository<CompletedProject> completedProjectRepository)
        {
            _completedProjectRepository = completedProjectRepository;
        }

        public async Task<List<CompletedProjectDTO>> getAll(CompletedProjectFilterDTO completedProjectFilterDTO)
        {
            var spec = new CompletedProjectGetAllSpec(completedProjectFilterDTO);

            var projects = await _completedProjectRepository.ListAsync(spec);

            return projects.Select(project => new CompletedProjectDTO(project)).ToList();
        }

        public async Task<CompletedProjectDTO> getById(int id, CompletedProjectFilterDTO completedProjectFilterDTO)
        {
            var spec = new CompletedProjectByIdSpec(id, completedProjectFilterDTO);

            var project = await _completedProjectRepository.FirstOrDefaultAsync(spec);

            if (project == null)
            {
                throw new Exception(ErrorConstants.ProjectNotFound);
            }

            return new CompletedProjectDTO(project);
        }
    }
}
