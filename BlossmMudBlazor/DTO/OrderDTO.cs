using BlossmAPI.Models;

namespace BlossmMudBlazor.DTO
{
    public class OrderDTO
    {
        public int id { get; set; }
        public DateTime createDate { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public int subtotal { get; set; }
        public object cancellationReason { get; set; }
        public string deliveryStatus { get; set; }
        public string idUser { get; set; }
        public string status { get; set; }
        public int? IdBranch { get; set; }
        public string? latlng { get; set; }
        public string? reciver { get; set; }
        public int? quantity { get; set; }
        public List<ProductVariantDTO> productVariants { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public Voucher voucher { get; set; }
        public string paymentMethod { get; set; }
    }
}
