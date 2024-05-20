namespace BlossmMudBlazor.DTO
{
    public class PurchaseOrder
    {
        public int id { get; set; }
        public DateTime orderDate { get; set; }
        public int? idPaymentMethod { get; set; }
        public string? namePaymentMethod { get; set; }
        public int idOrderStatus { get; set; }
        public string nameOrderStatus { get; set; }
        public string? idReceiver { get; set; }
        public string? lastNameReceiver { get; set; }
        public string? firstNameReceiver { get; set; }
        public DateTime? receivingDate { get; set; }
        public int idDeliveryStatus { get; set; }
        public string nameDeliveryStatus { get; set; }
        public DateTime? estimatedDeliveryDate { get; set; }
        public int idPurchaseRequest { get; set; }
        public string firstNameRequester { get; set; }
        public string lastNameRequester { get; set; }
        public int PRTotalAmount { get; set; }
        public DateTime requestDate { get; set; }
        public int? paymentAmount { get; set; }
        public string? note { get; set; }
    }
}
