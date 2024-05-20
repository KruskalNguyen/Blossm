using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Shift
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
