using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class TaskAssignment
{
    public int Id { get; set; }

    public string? IdUser { get; set; }

    public int? IdTask { get; set; }

    public DateTime? AsignDate { get; set; }

    public DateTime? Deadline { get; set; }

    public bool? Done { get; set; }

    public int? IdShift { get; set; }

    public virtual Shift? IdShiftNavigation { get; set; }

    public virtual EmployeeTask? IdTaskNavigation { get; set; }

    public virtual AspNetUser? IdUserNavigation { get; set; }
}
