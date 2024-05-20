using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class PurchaseRequestItem
{
    public int IdPurchaseRequest { get; set; }

    public int IdProductVariant { get; set; }

    public int? Quantity { get; set; }

    public virtual ProductVariant IdProductVariantNavigation { get; set; } = null!;

    public virtual PurchaseRequest IdPurchaseRequestNavigation { get; set; } = null!;
}
