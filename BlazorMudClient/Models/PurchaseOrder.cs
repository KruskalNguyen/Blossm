using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class PurchaseOrder
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? PaymentMethod { get; set; }

    public string? Receiver { get; set; }

    public DateTime? ReceivingDate { get; set; }

    public int? DeliveryStatus { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    public int? IdPurchaseRequest { get; set; }

    public int? OrderStatus { get; set; }

    public int? PaymentAmount { get; set; }

    public string? Note { get; set; }

    public virtual DeliveryStatus? DeliveryStatusNavigation { get; set; }

    public virtual PurchaseRequest? IdPurchaseRequestNavigation { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual OrderStatus? OrderStatusNavigation { get; set; }

    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    public virtual AspNetUser? ReceiverNavigation { get; set; }
}
