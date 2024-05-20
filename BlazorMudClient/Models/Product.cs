using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdCategory { get; set; }

    public string? ShortDescription { get; set; }

    public string? MainDescription { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
