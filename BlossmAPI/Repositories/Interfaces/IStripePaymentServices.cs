using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using Stripe.Checkout;

namespace BlossmAPI.Repositories.Interfaces
{
    public interface IStripePaymentServices
    {
        public Session CreateCheckoutSession(Order orderItems);
    }
}
