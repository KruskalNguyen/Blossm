using BlossmAPI.Attributes;
using BlossmAPI.Hubs;
using BlossmAPI.Hubs.Eintities;
using BlossmAPI.Patterns.Singleton;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly IWebSocketServices _webSocketServices;

        public WebSocketController(IWebSocketServices webSocketServices)
        {
            _webSocketServices = webSocketServices;
        }

        [HttpPost("ActiveOnline")]
        [BlossmAuthorize]
        public async Task<IActionResult> ActiveOnline(OnlineUser onlineUser)
        {
            await _webSocketServices.ActiveOnline(onlineUser);
            return Ok();
        }

        [HttpPost("NotiRequestPurchase")]
        [BlossmAuthorize]
        public async Task<IActionResult> NotiRequestPurchase(NotiPR noti)
        {
            await _webSocketServices.NotiRequestPurchase(noti);
            return Ok();
        }

        [HttpPost("ChatSupport")]
        public async Task<IActionResult> ChatSupport(Chat chat)
        {
            await _webSocketServices.ChatSupport(chat);
            return Ok();
        }


    }
}
