using SWP391API.Models;

namespace SWP391API.DTO
{
    public class ProjectResponse
    {
        public int ProjectId { get; set; }
        public int StyleId { get; set; }
        public int UserId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectImage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ProductResponse> products { get; set; }

        public ProjectResponse(CompletedProject p, List<ProductResponse> products)
        {
            ProjectId = p.ProjectId;
            StyleId = p.StyleId;
            UserId = p.UserId;
            ProjectTitle = p.ProjectTitle;
            ProjectDescription = p.ProjectDescription;
            ProjectImage = p.ProjectImage;
            StartDate = p.StartDate;
            EndDate = p.EndDate;
            this.products = products;
        }
    }
}
