using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class IssueImage
{
    public int Id { get; set; }

    public string? Base64 { get; set; }

    public int? IdProductIssue { get; set; }

    public virtual ProductVariant? IdProductIssueNavigation { get; set; }
}
