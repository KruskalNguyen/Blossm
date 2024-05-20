using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class EmployeeTask
{
    public int Id { get; set; }

    public string? TaskName { get; set; }

    public string? TaskDecs { get; set; }

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
