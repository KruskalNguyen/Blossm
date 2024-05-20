using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Newspaper
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? IdUser { get; set; }

    public string? Thumnail { get; set; }

    public virtual AspNetUser? IdUserNavigation { get; set; }
}
