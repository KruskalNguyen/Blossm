using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Employee? EmployeeIdUserNavigation { get; set; }

    public virtual ICollection<Employee> EmployeeManagerByNavigations { get; set; } = new List<Employee>();

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual ICollection<Newspaper>? Newspapers { get; set; } = new List<Newspaper>();

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductVariant>? ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseRequest> PurchaseRequestApproverNavigations { get; set; } = new List<PurchaseRequest>();

    public virtual ICollection<PurchaseRequest> PurchaseRequestRequesterNavigations { get; set; } = new List<PurchaseRequest>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    public virtual ICollection<Access> IdAccesses { get; set; } = new List<Access>();

    public virtual ICollection<Voucher> IdVouchers { get; set; } = new List<Voucher>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
