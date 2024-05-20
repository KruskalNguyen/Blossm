using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }

    public int? Subtotal { get; set; }

    public string? CancellationReason { get; set; }

    public int? DeliveryStatus { get; set; }

    public string? IdUser { get; set; }

    public int? IdStatus { get; set; }

    public int? IdUnregisterUser { get; set; }

    public int? IdPaymentMethod { get; set; }

    public int? IdBranch { get; set; }

    public string? Receiver { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Latlng { get; set; }

    public string? IdVoucher { get; set; }

    public virtual DeliveryStatus? DeliveryStatusNavigation { get; set; }

    public virtual Branch? IdBranchNavigation { get; set; }

    public virtual PaymentMethod? IdPaymentMethodNavigation { get; set; }

    public virtual OrderStatus? IdStatusNavigation { get; set; }

    public virtual UnregisteredUser? IdUnregisterUserNavigation { get; set; }

    public virtual AspNetUser? IdUserNavigation { get; set; }

    public virtual Voucher? IdVoucherNavigation { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
