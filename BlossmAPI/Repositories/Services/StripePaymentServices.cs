using Azure;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace BlossmAPI.Repositories.Services
{
    public class StripePaymentServices : IStripePaymentServices
    {
        private readonly BlossmContext _context;

        public StripePaymentServices(BlossmContext context)
        {
            _context = context;
            StripeConfiguration.ApiKey = "sk_test_51OJcvKG74kdHxVcY9A4C0D8VUXqCbDKBwB4gWzmuLLScqM1NebwEoHb7TvZyLxXaJ3neK6126f6xNvmDSrjPZvs800byaPZ96l";
        }

        public Session CreateCheckoutSession(Order order)
        {
            var lineItems = new List<SessionLineItemOptions>();
            foreach(var lineItem in order.OrderItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = lineItem.SellingPrice,
                        Currency = "vnd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {

                            Name = lineItem.IdProductVariantNavigation.IdProductNavigation.Name,
                            Images = new List<string> { "https://getuikit.com/v2/docs/images/placeholder_600x400.svg" }
                        }
                    },
                    Quantity = lineItem.Quantity
                });
            }

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"https://localhost:5001/PaymentSuccess/{order.Id}",
                CancelUrl = $"https://localhost:5001/PaymentCancel/{order.Id}",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
    }
}
