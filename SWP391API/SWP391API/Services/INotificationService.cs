using SWP391API.Models;

namespace SWP391API.Services
{
    public interface INotificationService
    {
        Task NewQuotationSubmitted(Quotation quotation);
        Task QuotationUpdated(Quotation quotation);
        Task QuotationStatusUpdated(Quotation quotation, string message);
    }
}
