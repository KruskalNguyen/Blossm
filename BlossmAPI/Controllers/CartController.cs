using BlossmAPI.Attributes;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BlossmAuthorize]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _services;
        private readonly IUserServices _userServices;
        public CartController(ICartServices services, IUserServices userServices)
        {
            _services = services;
            _userServices = userServices;
        }
        [HttpGet("GetCartById")]
        public async Task<IActionResult> GetCartById()
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var cartItems = await _services.GetShoppingCartByUserId(idUser);
            if (cartItems != null)
            {
                return Ok(cartItems);
            }
            return BadRequest();
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CartView view)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var result = await _services.AddVariantToCart(idUser, view);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpPut("AdjustVariantQuantity")]
        public async Task<IActionResult> AdjustVariantQuantity(CartView view)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var result = await _services.AdjustVariantQuantity(idUser, view);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpPut("IncreaseVariant")]
        public async Task<IActionResult> IncreaseVariant(CartView view)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var result = await _services.IncreaseVariant(idUser, view);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpPut("DecreaseVariant")]
        public async Task<IActionResult> DecreaseVariant(CartView view)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var result = await _services.DecreaseVariant(idUser, view);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpDelete("RemoveVariant")]
        public async Task<IActionResult> RemoveVariant([FromQuery] int id)
        {
            var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);
            var result = await _services.RemoveVariant(idUser, id);
            if (result) return Ok();
            return BadRequest();
        }
    }
}
