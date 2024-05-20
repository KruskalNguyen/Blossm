using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Voucher
{
    public string Id { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? DiscountPercentage { get; set; }

    public int? DiscountAmount { get; set; }

    public bool? Active { get; set; }

    public int? Limit { get; set; }

    public string? Name { get; set; }

    public int? Condition { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<AspNetUser>? IdUsers { get; set; } = new List<AspNetUser>();
}
