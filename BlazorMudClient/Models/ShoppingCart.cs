using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class ShoppingCart
{
    public string IdUser { get; set; } = null!;

    public int IdProductVariant { get; set; }

    public int? Quantity { get; set; }

    public DateTime? DateAdd { get; set; }

    public virtual ProductVariant IdProductVariantNavigation { get; set; } = null!;

    public virtual AspNetUser IdUserNavigation { get; set; } = null!;
}
