using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Image
{
    public int Id { get; set; }

    public string? Images { get; set; }

    public int? IdProductVariant { get; set; }

    public bool? Avatar { get; set; }

    public virtual ProductVariant? IdProductVariantNavigation { get; set; }
}
