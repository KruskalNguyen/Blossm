using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Page
{
    public int Id { get; set; }

    public string? UrlPage { get; set; }

    public int? ChildOf { get; set; }

    public virtual ICollection<Access> IdAccesses { get; set; } = new List<Access>();
}
