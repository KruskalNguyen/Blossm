using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class BranchProductVariant
{
    public int IdBranch { get; set; }

    public int IdProductVariant { get; set; }

    public int? InventoryQuantity { get; set; }

    public virtual Branch IdBranchNavigation { get; set; } = null!;

    public virtual ProductVariant? IdProductVariantNavigation { get; set; } = null!;
}
