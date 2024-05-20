using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? IdUser { get; set; }

    public int? IdProduct { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Content { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual AspNetUser? IdUserNavigation { get; set; }
}
