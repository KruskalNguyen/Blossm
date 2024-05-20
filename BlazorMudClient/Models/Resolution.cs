using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Resolution
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
