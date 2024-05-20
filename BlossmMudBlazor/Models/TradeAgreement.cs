using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class TradeAgreement
{
    public int Id { get; set; }

    public int? IdBranch { get; set; }

    public int? IdSupplier { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Requirements { get; set; }

    public string? ReturnPolicy { get; set; }

    public string? AgreementCreator { get; set; }

    public int? MinimumQuantity { get; set; }

    public bool? AgreementStatus { get; set; }

    public virtual Branch? IdBranchNavigation { get; set; }

    public virtual Supplier? IdSupplierNavigation { get; set; }

    public virtual ICollection<TradeAgreementAttachment> TradeAgreementAttachments { get; set; } = new List<TradeAgreementAttachment>();
}
