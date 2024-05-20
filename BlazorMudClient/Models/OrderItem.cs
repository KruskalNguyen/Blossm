using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class OrderItem
{
    public int IdOrder { get; set; }

    public int IdProductVariant { get; set; }

    public int? Quantity { get; set; }

    public int? SellingPrice { get; set; }

    public virtual Order? IdOrderNavigation { get; set; } = null!;

    public virtual ProductVariant? IdProductVariantNavigation { get; set; } = null!;
}
