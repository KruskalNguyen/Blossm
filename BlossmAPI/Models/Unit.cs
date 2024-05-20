using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ProductVariant>? ProductVariants { get; set; } = new List<ProductVariant>();
}
