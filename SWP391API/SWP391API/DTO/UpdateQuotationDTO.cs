using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class UpdateQuotationDTO
    {
        [Required]
        public int QuotationId { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public int? StyleId { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double? Witdh { get; set; }
        
        [Required]
        [Range(1, double.MaxValue)]
        public double? Height { get; set; }
        
        [Required]
        [Range(1, double.MaxValue)]
        public double? Length { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double? TotalConstructionCost { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double? TotalProductCost { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? HomeStyleId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? FloorConstructionId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? WallConstructId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? CeilingConstructId { get; set; }
        public List<QuotationDetailDTO> quotationDetailDTOs { get; set; }
    }
}
