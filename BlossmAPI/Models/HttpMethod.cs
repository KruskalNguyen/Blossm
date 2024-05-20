using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class HttpMethod
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();
}
