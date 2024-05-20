using BlossmAPI.Hubs.Eintities;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IWebSocketServices
    {
        Task SendMessage(Message message);
        Task ActiveOnline(OnlineUser onlineUser);
        Task NotiRequestPurchase(NotiPR notiPR);
        Task ChatSupport(Chat chat);
    }
}
