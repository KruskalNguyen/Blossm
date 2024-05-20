using BlazorMudClient.Models;
using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
    public class VnpayServices : IVnpayServices
    {
        public static string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public static string vnp_Api = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
        public static string vnp_TmnCode = "G2OHMOOX";
        public static string vnp_HashSecret = "UHVBRLDJNJZHHDMWLOFJFPPIUBDUCXFC";
        public static string vnp_Returnurl = "http://localhost:5001/PaymentSuccess";

        private readonly BlossmContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public VnpayServices(BlossmContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> CreatePaymentUrl(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            string? urlPayment = "";

            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            var OrderTotal = (long)order.Subtotal * 100;
            vnpay.AddRequestData("vnp_Amount", OrderTotal.ToString());
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            Utils utils = new Utils(_contextAccessor);
            vnpay.AddRequestData("vnp_IpAddr", utils.GetIpAddress());
            //vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.Id);
            vnpay.AddRequestData("vnp_OrderType", "other");

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl + $"/{order.Id}");
            vnpay.AddRequestData("vnp_TxnRef", order.Id.ToString());

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return urlPayment;
        }
    }
}
