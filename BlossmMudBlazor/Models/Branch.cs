using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public string? Manager { get; set; }

    public int? IdArea { get; set; }

    public string? Latlng { get; set; }

    public virtual ICollection<BranchProductVariant> BranchProductVariants { get; set; } = new List<BranchProductVariant>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Area? IdAreaNavigation { get; set; }

    public virtual AspNetUser? ManagerNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();

    public virtual ICollection<TradeAgreement> TradeAgreements { get; set; } = new List<TradeAgreement>();
}
