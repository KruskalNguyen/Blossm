using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class PurchaseRequestStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
