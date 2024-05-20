using BlossmAPI.Hubs.Eintities;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Net.Sockets;

namespace BlossmAPI.Hubs
{
    public class BlossmHub : Hub
    {
        public static List<string> idCLients = new List<string>();
        public override async Task OnConnectedAsync()
        {
            idCLients.Add(Context.ConnectionId);

            await Clients.Client(Context.ConnectionId).SendAsync("id", Context.ConnectionId);
            await Clients.All.SendAsync("listId", idCLients);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            idCLients.Remove(Context.ConnectionId);
            ListClient.onlineUsers.Remove(ListClient.onlineUsers.FirstOrDefault(o => o.idClient == Context.ConnectionId));
            await Clients.All.SendAsync("ListUserOnline", ListClient.onlineUsers);
            await Clients.All.SendAsync("listId", idCLients);
        }
    }
}
