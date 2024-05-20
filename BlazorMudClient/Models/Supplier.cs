using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }

    public int? Rating { get; set; }

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();

    public virtual ICollection<SupplierContact> SupplierContacts { get; set; } = new List<SupplierContact>();

    public virtual ICollection<TradeAgreement> TradeAgreements { get; set; } = new List<TradeAgreement>();

    public virtual ICollection<ProductVariant> IdProductVariants { get; set; } = new List<ProductVariant>();
}
