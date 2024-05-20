using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Priority
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
