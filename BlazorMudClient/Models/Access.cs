using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class Access
{
    public int Id { get; set; }

    public string? Action { get; set; }

    public string? UrlApi { get; set; }

    public int? HttpMethod { get; set; }

    public virtual HttpMethod? HttpMethodNavigation { get; set; }

    public virtual ICollection<Page> IdPages { get; set; } = new List<Page>();

    public virtual ICollection<AspNetRole> IdRoles { get; set; } = new List<AspNetRole>();

    public virtual ICollection<AspNetUser> IdUsers { get; set; } = new List<AspNetUser>();
}
