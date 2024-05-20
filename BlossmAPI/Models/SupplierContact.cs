using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class SupplierContact
{
    public int Id { get; set; }

    public string? ContactPerson { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? IdSupplier { get; set; }

    public virtual Supplier? IdSupplierNavigation { get; set; }
}
