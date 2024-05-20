using BlossmAPI.Hubs;
using BlossmAPI.Hubs.Eintities;
using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class WebSocketServices:IWebSocketServices
    {
        private IHubContext<BlossmHub> _hubContext;
        private readonly BlossmContext _blossmContext;

        public WebSocketServices(IHubContext<BlossmHub> hubContext, BlossmContext blossmContext)
        {
            _hubContext = hubContext;
            _blossmContext = blossmContext;
        }

        public async Task SendMessage(Message message)
        {
            await _hubContext.Clients.Client(message.Id).SendAsync("SendMess", $"{DateTime.Now}: {message.Title}");
        }

        public async Task ActiveOnline(OnlineUser onlineUser)
        {
            onlineUser.startTime = DateTime.Now;

            var branch = await _blossmContext.Branches.FirstOrDefaultAsync(b => b.Manager == onlineUser.idUser);

            if( branch != null )
                onlineUser.isBranchManager = true;

            ListClient.onlineUsers.Add(onlineUser);
            var debug = ListClient.onlineUsers;
            await _hubContext.Clients.All.SendAsync("ListUserOnline", ListClient.onlineUsers);
        }

        public async Task NotiRequestPurchase(NotiPR notiPR)
        {
            await _hubContext.Clients.Client(notiPR.idClient).SendAsync("NotiRequestPurchase", $"{notiPR.action} by {notiPR.fullname} Time: {notiPR.dateTime}");
        }

        public async Task ChatSupport(Chat chat)
        {
            await _hubContext.Clients.Client(chat.send_to).SendAsync("ChatSupport", chat);
        }
    }
}
