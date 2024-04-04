using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SWP391API.Services;
using SWP391API.Services.Implements;

namespace SWP391API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {

        private readonly IAuthenticateService _authenticateService;

        public ChatHub(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task JoinChatRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task SendMessage(string roomId, string message)
        {
            var username = _authenticateService.getCurrentUsername();

            await Clients.Group(roomId).SendAsync("ReceiveMessage", username!, message);
        }
    }
}
