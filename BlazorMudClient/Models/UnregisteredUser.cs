using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class UnregisteredUser
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
