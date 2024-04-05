using Microsoft.AspNetCore.Mvc;
using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Repositories;
using SWP391API.Specifications;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly Repository<Quotation> _quotationRepository;
        private readonly Repository<User> _userRepository;

        public GraphController(Repository<Quotation> quotationRepository, Repository<User> userRepository)
        {
            _quotationRepository = quotationRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGraph([FromQuery] GraphFilterDTO graphFilterDTO)
        {
            List<GraphMonthReportDTO> graphMonthReportDTOs = new List<GraphMonthReportDTO>();

            for(int i = 1; i <= 12; i++ )
            {
                var countPendingQuotationSpec = new QuotationByStatusAndDateSpec("Pending", i, graphFilterDTO.Year);
                var countPendingQuotation = await _quotationRepository.CountAsync(countPendingQuotationSpec);

                var countDoneQuotationSpec = new QuotationByStatusAndDateSpec("Done", i, graphFilterDTO.Year);
                var countDoneQuotation = await _quotationRepository.CountAsync(countDoneQuotationSpec);

                var countCancelledQuotationSpec = new QuotationByStatusAndDateSpec("Cancel", i, graphFilterDTO.Year);
                var countCancelledQuotation = await _quotationRepository.CountAsync(countCancelledQuotationSpec);

                var countActivatedUserSepc = new UserByStatusAndDateSpec(true, i, graphFilterDTO.Year);
                var countActivatedUser = await _userRepository.CountAsync(countActivatedUserSepc);

                var countDeactivatedUserSepc = new UserByStatusAndDateSpec(false, i, graphFilterDTO.Year);
                var countDeactivatedUser = await _userRepository.CountAsync(countDeactivatedUserSepc);

                GraphMonthReportDTO graphMonthReportDTO = new GraphMonthReportDTO();

                graphMonthReportDTO.TotalQuotationCount = countPendingQuotation + countDoneQuotation + countCancelledQuotation;
                graphMonthReportDTO.PendingQuotationCount = countPendingQuotation;
                graphMonthReportDTO.DoneQuotationCount = countDoneQuotation;
                graphMonthReportDTO.CancelledQuotationCount = countCancelledQuotation;

                graphMonthReportDTO.ActivatedUserCount = countActivatedUser;
                graphMonthReportDTO.DeactivatedUserCount = countDeactivatedUser;
                graphMonthReportDTO.TotalUserCount = countActivatedUser + countDeactivatedUser;

                graphMonthReportDTOs.Add(graphMonthReportDTO);
                
            }

            return Ok(graphMonthReportDTOs);
        }
    }
}
