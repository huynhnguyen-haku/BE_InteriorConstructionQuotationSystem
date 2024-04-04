using Microsoft.AspNetCore.SignalR;
using SWP391API.Hubs;
using SWP391API.Models;
using SWP391API.Repositories;
using SWP391API.Specifications;
using SWP391API.Utilities;

namespace SWP391API.Services.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly Repository<User> _userRepository;
        private readonly IEmailService _emailService;

        public NotificationService(IHubContext<NotificationHub> hubContext, Repository<User> userRepository, IEmailService emailService)
        {
            _hubContext = hubContext;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task NewQuotationSubmitted(Quotation quotation)
        {
            var userSpec = new UserByRoleNameSpec(RoleConstants.Staff); //get all staffs
            var staffs = await _userRepository.ListAsync(userSpec);

            var staffUsernames = staffs.ConvertAll(s => s.Username);

            await _hubContext.Clients.Users(staffUsernames).SendAsync("NewQuotationSubmitted", quotation.QuotationId, quotation.CreatedAt);

            foreach (var staff in staffs)
            {
                _emailService.sendTo(staff.Email, "New Quotation Submitted", $"A new quotation has been submitted at {quotation.CreatedAt} with id {quotation.QuotationId}");
            }

        }

        public async Task QuotationStatusUpdated(Quotation quotation, string message)
        {
            await _hubContext.Clients.User(quotation.User.Username).SendAsync("QuotationStatusUpdated", quotation.QuotationId, quotation.QuotationStatus, DateTime.Now, message);

            _emailService.sendTo(quotation.User.Email, "Quotation Status Updated", $"Your quotation with id {quotation.QuotationId} has been updated to {quotation.QuotationStatus} at {DateTime.Now}. Message: {message}");
        }

        public async Task QuotationUpdated(Quotation quotation)
        {
            await _hubContext.Clients.User(quotation.User.Username).SendAsync("QuotationUpdated", quotation.QuotationId, DateTime.Now);

            _emailService.sendTo(quotation.User.Email, "Quotation Updated", $"Your quotation with id {quotation.QuotationId} has been updated at {DateTime.Now}");
        }
    }
}
