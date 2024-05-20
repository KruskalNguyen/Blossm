using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<Access> IdAccesses { get; set; } = new List<Access>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
