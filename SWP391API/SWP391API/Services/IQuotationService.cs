using SWP391API.DTO;

namespace SWP391API.Services
{
    public interface IQuotationService
    {
        Task<List<QuotationResponseDTO>> getQuotationsOfUser(int userId, QuotationFilterDTO quotationFilterDTO);
        Task<List<QuotationResponseDTO>> getAllQuotations(QuotationFilterDTO quotationFilterDTO);
        Task<QuotationResponseDTO> createQuotation(int userId, SubmitQuotationDTO req);

        Task updateQuotationStatus(QuotationUpdateStatusDTO req);

        Task<QuotationResponseDTO> updateQuotation(UpdateQuotationDTO updateQuotationDTO);

        Task sendFinalQuotationContractToUser(int quotationId);
    }
}
