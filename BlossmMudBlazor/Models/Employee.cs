using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Employee
{
    public string IdUser { get; set; } = null!;

    public int IdBranch { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Active { get; set; }

    public virtual Branch IdBranchNavigation { get; set; } = null!;

    public virtual AspNetUser IdUserNavigation { get; set; } = null!;
}
