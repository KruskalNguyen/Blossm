using BlossmAPI.Models;
using System.Net;

namespace BlossmAPI.ModelViews
{
    public class OrderView
    {
        public int id { get; set; }
        public string? address { get; set; }
        public string? username { get; set; }
        public string? phoneNumber { get; set; }
        public string? note { get; set; } = "";
        public string? id_user { get; set; }
        public int? id_status { get; set; }
        public int? id_delivery_status { get; set; }
        public int? id_payment { get; set; }
        public string? cancellation_reason { get; set; }
        public int? totalValue { get; set; }
        public Voucher? voucher { get; set; }
        public List<OrderItemView> orderItems { get; set; } = new List<OrderItemView>();
    }
}
