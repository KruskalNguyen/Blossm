using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public int? IdUnit { get; set; }

    public int? IdColor { get; set; }

    public int? IdSize { get; set; }

    public int? PurchasePrice { get; set; }

    public int? SellingPrice { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public bool? Publish { get; set; }

    public virtual ICollection<BranchProductVariant> BranchProductVariants { get; set; } = new List<BranchProductVariant>();

    public virtual AspNetUser? CreateByNavigation { get; set; }

    public virtual Color? IdColorNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Size? IdSizeNavigation { get; set; }

    public virtual Unit? IdUnitNavigation { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<IssueImage> IssueImages { get; set; } = new List<IssueImage>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PurchaseRequestItem> PurchaseRequestItems { get; set; } = new List<PurchaseRequestItem>();

    public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<Supplier> IdSuppliers { get; set; } = new List<Supplier>();
}
