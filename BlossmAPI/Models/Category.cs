using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ChildOf { get; set; }

    public string? CategoryDescription { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
