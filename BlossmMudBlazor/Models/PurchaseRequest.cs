using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class PurchaseRequest
{
    public int Id { get; set; }

    public string? Requester { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? Deadline { get; set; }

    public int? Status { get; set; }

    public int? Priority { get; set; }

    public string? Notes { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public int? TotalQuantity { get; set; }

    public int? TotalAmount { get; set; }

    public string? Approver { get; set; }

    public int? IdBranch { get; set; }

    public int? IdSupplier { get; set; }

    public virtual AspNetUser? ApproverNavigation { get; set; }

    public virtual Branch? IdBranchNavigation { get; set; }

    public virtual Supplier? IdSupplierNavigation { get; set; }

    public virtual Priority? PriorityNavigation { get; set; }

    public virtual PurchaseOrder? PurchaseOrder { get; set; }

    public virtual ICollection<PurchaseRequestItem> PurchaseRequestItems { get; set; } = new List<PurchaseRequestItem>();

    public virtual AspNetUser? RequesterNavigation { get; set; }

    public virtual PurchaseRequestStatus? StatusNavigation { get; set; }
}
