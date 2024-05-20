using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Issue
{
    public int Id { get; set; }

    public DateTime? DateRaised { get; set; }

    public string? RecordedBy { get; set; }

    public DateTime? ResolutionDate { get; set; }

    public string? IssueDescription { get; set; }

    public int? Resolution { get; set; }

    public int? IdPurchaseOrder { get; set; }

    public virtual PurchaseOrder? IdPurchaseOrderNavigation { get; set; }

    public virtual AspNetUser? RecordedByNavigation { get; set; }

    public virtual Resolution? ResolutionNavigation { get; set; }
}
