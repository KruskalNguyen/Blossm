using BlossmMudBlazor.DTO;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlossmMudBlazor.Helper
{
    public class WebSocket
    {
        public static List<string> idsClient = new List<string>();
        public static string? idClient;
        public static HubConnection connection = null;
        public static bool isConnected = false;
        public static string connectionStatus = "Closed";

        public static List<OnlineUser> onlineUsers = new List<OnlineUser>();
    }
}
