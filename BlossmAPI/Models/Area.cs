using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Area
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
