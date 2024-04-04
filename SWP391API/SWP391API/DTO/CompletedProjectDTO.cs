using SWP391API.Models;

namespace SWP391API.DTO
{
    public class CompletedProjectDTO
    {
        public int ProjectId { get; set; }
        public int StyleId { get; set; }
        public int UserId { get; set; }
        public string ProjectTitle { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;

        public string ProjectInformation { get; set; } = null!;

        public string ProjectResult { get; set; } = null!;

        public string Location { get; set; } = null!;

        public double SurfaceArea { get; set; }

        public string ProjectImage { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ProductInProjectDTO> ProductsInProject { get; set; } 

        public CompletedProjectDTO(CompletedProject project)
        {
            ProjectId = project.ProjectId;
            StyleId = project.StyleId;
            UserId = project.UserId;
            ProjectTitle = project.ProjectTitle;

            ProjectInformation = project.ProjectInformation;
            ProjectResult = project.ProjectResult;
            Location = project.Location;
            SurfaceArea = project.SurfaceArea;

            ProjectDescription = project.ProjectDescription;
            ProjectImage = project.ProjectImage;
            StartDate = (DateTime)project.StartDate;
            EndDate = (DateTime)project.EndDate;

            ProductsInProject = new List<ProductInProjectDTO>();
            foreach (var product in project.ProductInProjects)
            {
                ProductsInProject.Add(new ProductInProjectDTO(product));
            }
        }

    }
}
