using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IStripePaymentServices _stripePaymentServices;
        public StripePaymentController(IStripePaymentServices stripePaymentServices)
        {
            _stripePaymentServices = stripePaymentServices;
        }
        [HttpPost("checkout")]
        public ActionResult CreateCheckoutSession(Order orderItems)
        {
            var session = _stripePaymentServices.CreateCheckoutSession(orderItems);
            return Ok(session.Url);
        }
    }
}
