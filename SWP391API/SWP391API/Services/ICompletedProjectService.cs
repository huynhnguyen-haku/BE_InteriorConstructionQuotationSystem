using SWP391API.DTO;

namespace SWP391API.Services
{
    public interface ICompletedProjectService 
    {
        public Task<List<CompletedProjectDTO>> getAll(CompletedProjectFilterDTO completedProjectFilterDTO);
        public Task<CompletedProjectDTO> getById(int id, CompletedProjectFilterDTO completedProjectFilterDTO);
    }
}
