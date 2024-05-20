using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Repositories.Services;

namespace BlossmAPI.Configuations
{
    public sealed class ConfigDependencyInjection
    {
        public static void BuilderConfig(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUserServices, UserServices>();
            builder.Services.AddTransient<IProductServices, ProductServices>();
            builder.Services.AddTransient<IProductVariantServices, ProductServices>();
            builder.Services.AddTransient<IImageServices, ImageServices>();
            builder.Services.AddTransient<ISupplierServices, SupplierServices>();
            builder.Services.AddTransient<IAuthorizeServices, AuthorizeService>();
            builder.Services.AddTransient<IPurchaseServices, PurchaseServices>();
            builder.Services.AddTransient<ISizeServices, SizeServices>();
            builder.Services.AddTransient<IUnitServices, UnitServices>();
            builder.Services.AddTransient<IColorServices, ColorServices>();
            builder.Services.AddTransient<ICartServices, CartServices>();
            builder.Services.AddTransient<IAreaService, AreaService>();
            builder.Services.AddTransient<IBranchService, BranchService>();
            builder.Services.AddTransient<IBranchProductVariantService, BranchProductVariantService>();
            builder.Services.AddTransient<IOrderServices, OrderServices>();
            builder.Services.AddTransient<IPurchaseRequestStatusInterfaces, PurchaseRequestStatusServices>();
            builder.Services.AddTransient<IPriovityServices, PriovityServices>();
            builder.Services.AddTransient<IWebSocketServices, WebSocketServices>();
            builder.Services.AddTransient<ICategoryServices, CategoryServices>();
            builder.Services.AddTransient<IDeliveryStatusServices, DeliveryStatusServices>();
            builder.Services.AddTransient<IOrderStatusServices, OrderStatusServices>();
            builder.Services.AddTransient<IPaymentMethodServieces, PaymentMethodServieces>();
            builder.Services.AddTransient<IRevenueServices, RevenueServices>();
            builder.Services.AddTransient<IStripePaymentServices, StripePaymentServices>();
            builder.Services.AddTransient<IVoucherServices, VoucherServices>();
            builder.Services.AddTransient<ICommentServices, CommentServices>();
            builder.Services.AddTransient<IVnpayServices, VnpayServices>();
            builder.Services.AddTransient<INewspaperServices, NewspaperServices>();
        }
    }
}
