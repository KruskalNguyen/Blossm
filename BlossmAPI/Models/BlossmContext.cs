using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Models;

public partial class BlossmContext : DbContext
{
    public BlossmContext()
    {
    }

    public BlossmContext(DbContextOptions<BlossmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchProductVariant> BranchProductVariants { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }

    public virtual DbSet<HttpMethod> HttpMethods { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueImage> IssueImages { get; set; }

    public virtual DbSet<Newspaper> Newspapers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseRequest> PurchaseRequests { get; set; }

    public virtual DbSet<PurchaseRequestItem> PurchaseRequestItems { get; set; }

    public virtual DbSet<PurchaseRequestStatus> PurchaseRequestStatuses { get; set; }

    public virtual DbSet<Resolution> Resolutions { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierContact> SupplierContacts { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<TradeAgreement> TradeAgreements { get; set; }

    public virtual DbSet<TradeAgreementAttachment> TradeAgreementAttachments { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnregisteredUser> UnregisteredUsers { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-M6QE4CK\\SQLEXPRESS;Database=Blossm;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Access__3213E83F4B469995");

            entity.ToTable("Access");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.HttpMethod).HasColumnName("http_method");
            entity.Property(e => e.UrlApi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("url_api");

            entity.HasOne(d => d.HttpMethodNavigation).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.HttpMethod)
                .HasConstraintName("FK__Access__http_met__17F790F9");

            entity.HasMany(d => d.IdPages).WithMany(p => p.IdAccesses)
                .UsingEntity<Dictionary<string, object>>(
                    "AccessPage",
                    r => r.HasOne<Page>().WithMany()
                        .HasForeignKey("IdPage")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AccessPag__id_pa__19DFD96B"),
                    l => l.HasOne<Access>().WithMany()
                        .HasForeignKey("IdAccess")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AccessPag__id_ac__18EBB532"),
                    j =>
                    {
                        j.HasKey("IdAccess", "IdPage").HasName("PK__AccessPa__42CD2649960E8642");
                        j.ToTable("AccessPage");
                        j.IndexerProperty<int>("IdAccess").HasColumnName("id_access");
                        j.IndexerProperty<int>("IdPage").HasColumnName("id_page");
                    });

            entity.HasMany(d => d.IdRoles).WithMany(p => p.IdAccesses)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleAccess",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleAcces__id_ro__4B7734FF"),
                    l => l.HasOne<Access>().WithMany()
                        .HasForeignKey("IdAccess")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleAcces__id_ac__4A8310C6"),
                    j =>
                    {
                        j.HasKey("IdAccess", "IdRole").HasName("PK__RoleAcce__318DB90F00D653AE");
                        j.ToTable("RoleAccess");
                        j.IndexerProperty<int>("IdAccess").HasColumnName("id_access");
                        j.IndexerProperty<string>("IdRole").HasColumnName("id_role");
                    });

            entity.HasMany(d => d.IdUsers).WithMany(p => p.IdAccesses)
                .UsingEntity<Dictionary<string, object>>(
                    "AccessUser",
                    r => r.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AccessUse__id_us__1BC821DD"),
                    l => l.HasOne<Access>().WithMany()
                        .HasForeignKey("IdAccess")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AccessUse__id_ac__1AD3FDA4"),
                    j =>
                    {
                        j.HasKey("IdAccess", "IdUser");
                        j.ToTable("AccessUser");
                        j.IndexerProperty<int>("IdAccess").HasColumnName("id_access");
                        j.IndexerProperty<string>("IdUser").HasColumnName("id_user");
                    });
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Area__3213E83F9B707B3E");

            entity.ToTable("Area");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Branch__3213E83F9D367CA7");

            entity.ToTable("Branch");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(450)
                .HasColumnName("address");
            entity.Property(e => e.IdArea).HasColumnName("id_area");
            entity.Property(e => e.Latlng).HasColumnName("latlng");
            entity.Property(e => e.Manager)
                .HasMaxLength(450)
                .HasColumnName("manager");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Branch__id_area__22751F6C");

            entity.HasOne(d => d.ManagerNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.Manager)
                .HasConstraintName("FK__Branch__manager__236943A5");
        });

        modelBuilder.Entity<BranchProductVariant>(entity =>
        {
            entity.HasKey(e => new { e.IdBranch, e.IdProductVariant }).HasName("PK__BranchPr__0B65E8E5992B8364");

            entity.ToTable("BranchProductVariant");

            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.IdProductVariant).HasColumnName("id_product_variant");
            entity.Property(e => e.InventoryQuantity).HasColumnName("inventory_quantity");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.BranchProductVariants)
                .HasForeignKey(d => d.IdBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BranchPro__id_br__245D67DE");

            entity.HasOne(d => d.IdProductVariantNavigation).WithMany(p => p.BranchProductVariants)
                .HasForeignKey(d => d.IdProductVariant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BranchPro__id_pr__25518C17");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryDescription).HasColumnName("category_description");
            entity.Property(e => e.ChildOf).HasColumnName("child_of");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.ToTable("Color");

            entity.Property(e => e.Hex)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("hex");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3213E83F9D3B87C8");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_Comment_Product");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<DeliveryStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83FD0AFCA01");

            entity.ToTable("DeliveryStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdBranch });

            entity.ToTable("Employee");

            entity.HasIndex(e => e.IdUser, "UQ__Employee__D2D1463640988CA5").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.ManagerBy)
                .HasMaxLength(450)
                .HasColumnName("manager_by");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__id_bra__282DF8C2");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.EmployeeIdUserNavigation)
                .HasForeignKey<Employee>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__id_use__29221CFB");

            entity.HasOne(d => d.ManagerByNavigation).WithMany(p => p.EmployeeManagerByNavigations)
                .HasForeignKey(d => d.ManagerBy)
                .HasConstraintName("FK__Employee__manage__2A164134");
        });

        modelBuilder.Entity<EmployeeTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FB363725C");

            entity.ToTable("EmployeeTask");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TaskDecs).HasColumnName("task_decs");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .HasColumnName("task_name");
        });

        modelBuilder.Entity<HttpMethod>(entity =>
        {
            entity.ToTable("HttpMethod");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image__3213E83FCC04E03F");

            entity.ToTable("Image");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProductVariant).HasColumnName("Id_ProductVariant");
            entity.Property(e => e.Images).IsUnicode(false);

            entity.HasOne(d => d.IdProductVariantNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.IdProductVariant)
                .HasConstraintName("FK_Image_ProductVariant");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.ToTable("Issue");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateRaised)
                .HasColumnType("datetime")
                .HasColumnName("date_raised");
            entity.Property(e => e.IdPurchaseOrder).HasColumnName("id_purchase_order");
            entity.Property(e => e.IssueDescription).HasColumnName("issue_description");
            entity.Property(e => e.RecordedBy)
                .HasMaxLength(450)
                .HasColumnName("recorded_by");
            entity.Property(e => e.Resolution).HasColumnName("resolution");
            entity.Property(e => e.ResolutionDate)
                .HasColumnType("datetime")
                .HasColumnName("resolution_date");

            entity.HasOne(d => d.IdPurchaseOrderNavigation).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IdPurchaseOrder)
                .HasConstraintName("FK_Issue_PurchaseOrder");

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.Issues)
                .HasForeignKey(d => d.RecordedBy)
                .HasConstraintName("FK_Issue_AspNetUsers");

            entity.HasOne(d => d.ResolutionNavigation).WithMany(p => p.Issues)
                .HasForeignKey(d => d.Resolution)
                .HasConstraintName("FK_Issue_Resolution");
        });

        modelBuilder.Entity<IssueImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IssueIma__3213E83FA512A01B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64).HasColumnName("base64");
            entity.Property(e => e.IdProductIssue).HasColumnName("id_product_issue");

            entity.HasOne(d => d.IdProductIssueNavigation).WithMany(p => p.IssueImages)
                .HasForeignKey(d => d.IdProductIssue)
                .HasConstraintName("FK_IssueImages_ProductIssue");
        });

        modelBuilder.Entity<Newspaper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Newspape__3213E83F7845F8DD");

            entity.ToTable("Newspaper");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");
            entity.Property(e => e.Thumnail).HasColumnName("thumnail");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Newspapers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Newspaper__id_us__625A9A57");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83F7C598B28");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.CancellationReason).HasColumnName("cancellation_reason");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DeliveryStatus).HasColumnName("delivery_status");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.IdPaymentMethod).HasColumnName("id_payment_method");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdUnregisterUser).HasColumnName("id_unregister_user");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");
            entity.Property(e => e.IdVoucher)
                .HasMaxLength(6)
                .HasColumnName("id_voucher");
            entity.Property(e => e.Latlng).HasColumnName("latlng");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Receiver)
                .HasMaxLength(100)
                .HasColumnName("receiver");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.DeliveryStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryStatus)
                .HasConstraintName("FK__Orders__delivery__31B762FC");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdBranch)
                .HasConstraintName("FK__Orders__id_branc__32AB8735");

            entity.HasOne(d => d.IdPaymentMethodNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdPaymentMethod)
                .HasConstraintName("FK__Orders__id_payme__339FAB6E");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK__Orders__id_statu__3493CFA7");

            entity.HasOne(d => d.IdUnregisterUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUnregisterUser)
                .HasConstraintName("FK__Orders__id_unreg__3587F3E0");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Orders__id_user__367C1819");

            entity.HasOne(d => d.IdVoucherNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdVoucher)
                .HasConstraintName("FK__Orders__id_vouch__37703C52");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.IdOrder, e.IdProductVariant });

            entity.ToTable("OrderItem");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdProductVariant).HasColumnName("id_product_variant");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SellingPrice).HasColumnName("selling_price");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__id_or__2FCF1A8A");

            entity.HasOne(d => d.IdProductVariantNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.IdProductVariant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__id_pr__30C33EC3");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.ToTable("Page");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChildOf).HasColumnName("child_of");
            entity.Property(e => e.UrlPage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("url_page");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3213E83FBB80454E");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Priority__3213E83FA445BA34");

            entity.ToTable("Priority");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Note).HasColumnName("note");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.MainDescription).HasColumnName("main_description");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(255)
                .HasColumnName("short_description");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.ToTable("ProductVariant");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(450)
                .HasColumnName("create_by");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.IdColor).HasColumnName("id_color");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdSize).HasColumnName("id_size");
            entity.Property(e => e.IdUnit).HasColumnName("id_unit");
            entity.Property(e => e.Publish).HasColumnName("publish");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.SellingPrice).HasColumnName("selling_price");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_ProductVariant_AspNetUsers");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("FK_ProductVariant_Color");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_ProductVariant_Product");

            entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.IdSize)
                .HasConstraintName("FK_ProductVariant_Size");

            entity.HasOne(d => d.IdUnitNavigation).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.IdUnit)
                .HasConstraintName("FK_ProductVariant_Unit");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3213E83FB44AE052");

            entity.ToTable("PurchaseOrder");

            entity.HasIndex(e => e.IdPurchaseRequest, "UQ__Purchase__D19AE043ADCAF349").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryStatus).HasColumnName("delivery_status");
            entity.Property(e => e.EstimatedDeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("estimated_delivery_date");
            entity.Property(e => e.IdPurchaseRequest).HasColumnName("id_purchase_request");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.PaymentAmount).HasColumnName("payment_amount");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.Receiver)
                .HasMaxLength(450)
                .HasColumnName("receiver");
            entity.Property(e => e.ReceivingDate)
                .HasColumnType("datetime")
                .HasColumnName("receiving_date");

            entity.HasOne(d => d.DeliveryStatusNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.DeliveryStatus)
                .HasConstraintName("FK__PurchaseO__deliv__3E1D39E1");

            entity.HasOne(d => d.IdPurchaseRequestNavigation).WithOne(p => p.PurchaseOrder)
                .HasForeignKey<PurchaseOrder>(d => d.IdPurchaseRequest)
                .HasConstraintName("FK__PurchaseO__id_pu__3F115E1A");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.OrderStatus)
                .HasConstraintName("FK__PurchaseO__order__40058253");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.PaymentMethod)
                .HasConstraintName("FK__PurchaseO__payme__40F9A68C");

            entity.HasOne(d => d.ReceiverNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.Receiver)
                .HasConstraintName("FK__PurchaseO__recei__41EDCAC5");
        });

        modelBuilder.Entity<PurchaseRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3213E83F687ED218");

            entity.ToTable("PurchaseRequest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalDate)
                .HasColumnType("datetime")
                .HasColumnName("approval_date");
            entity.Property(e => e.Approver)
                .HasMaxLength(450)
                .HasColumnName("approver");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.IdSupplier).HasColumnName("id_supplier");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Requester)
                .HasMaxLength(450)
                .HasColumnName("requester");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.TotalQuantity).HasColumnName("total_quantity");

            entity.HasOne(d => d.ApproverNavigation).WithMany(p => p.PurchaseRequestApproverNavigations)
                .HasForeignKey(d => d.Approver)
                .HasConstraintName("FK__PurchaseR__appro__42E1EEFE");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.PurchaseRequests)
                .HasForeignKey(d => d.IdBranch)
                .HasConstraintName("FK__PurchaseR__id_br__43D61337");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.PurchaseRequests)
                .HasForeignKey(d => d.IdSupplier)
                .HasConstraintName("FK__PurchaseR__id_su__44CA3770");

            entity.HasOne(d => d.PriorityNavigation).WithMany(p => p.PurchaseRequests)
                .HasForeignKey(d => d.Priority)
                .HasConstraintName("FK__PurchaseR__prior__45BE5BA9");

            entity.HasOne(d => d.RequesterNavigation).WithMany(p => p.PurchaseRequestRequesterNavigations)
                .HasForeignKey(d => d.Requester)
                .HasConstraintName("FK__PurchaseR__reque__46B27FE2");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.PurchaseRequests)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK__PurchaseR__statu__47A6A41B");
        });

        modelBuilder.Entity<PurchaseRequestItem>(entity =>
        {
            entity.HasKey(e => new { e.IdPurchaseRequest, e.IdProductVariant });

            entity.ToTable("PurchaseRequestItem");

            entity.Property(e => e.IdPurchaseRequest).HasColumnName("id_purchase_request");
            entity.Property(e => e.IdProductVariant).HasColumnName("id_product_variant");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdProductVariantNavigation).WithMany(p => p.PurchaseRequestItems)
                .HasForeignKey(d => d.IdProductVariant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseR__id_pr__489AC854");

            entity.HasOne(d => d.IdPurchaseRequestNavigation).WithMany(p => p.PurchaseRequestItems)
                .HasForeignKey(d => d.IdPurchaseRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseR__id_pu__498EEC8D");
        });

        modelBuilder.Entity<PurchaseRequestStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3213E83F8AB0FC81");

            entity.ToTable("PurchaseRequestStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Resolution>(entity =>
        {
            entity.ToTable("Resolution");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shift__3213E83F4809DEBD");

            entity.ToTable("Shift");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdProductVariant });

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdProductVariant).HasColumnName("id_product_variant");
            entity.Property(e => e.DateAdd)
                .HasColumnType("datetime")
                .HasColumnName("date_add");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdProductVariantNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdProductVariant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShoppingC__id_pr__4C6B5938");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShoppingC__id_us__4D5F7D71");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3213E83F0728C808");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(450)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasMany(d => d.IdProductVariants).WithMany(p => p.IdSuppliers)
                .UsingEntity<Dictionary<string, object>>(
                    "SupplierProductVariant",
                    r => r.HasOne<ProductVariant>().WithMany()
                        .HasForeignKey("IdProductVariant")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SupplierP__id_pr__4F47C5E3"),
                    l => l.HasOne<Supplier>().WithMany()
                        .HasForeignKey("IdSupplier")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SupplierP__id_su__503BEA1C"),
                    j =>
                    {
                        j.HasKey("IdSupplier", "IdProductVariant");
                        j.ToTable("SupplierProductVariant");
                        j.IndexerProperty<int>("IdSupplier").HasColumnName("id_supplier");
                        j.IndexerProperty<int>("IdProductVariant").HasColumnName("id_product_variant");
                    });
        });

        modelBuilder.Entity<SupplierContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3213E83FF74E0BB3");

            entity.ToTable("SupplierContact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .HasColumnName("contact_person");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IdSupplier).HasColumnName("id_supplier");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.SupplierContacts)
                .HasForeignKey(d => d.IdSupplier)
                .HasConstraintName("FK__SupplierC__id_su__4E53A1AA");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskAssi__3213E83F564612AC");

            entity.ToTable("TaskAssignment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AsignDate)
                .HasColumnType("datetime")
                .HasColumnName("asign_date");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Done).HasColumnName("done");
            entity.Property(e => e.IdShift).HasColumnName("id_shift");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("id_user");

            entity.HasOne(d => d.IdShiftNavigation).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.IdShift)
                .HasConstraintName("FK__TaskAssig__id_sh__51300E55");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.IdTask)
                .HasConstraintName("FK__TaskAssig__id_ta__5224328E");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__TaskAssig__id_us__531856C7");
        });

        modelBuilder.Entity<TradeAgreement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TradeAgr__3213E83F5ED7B5EE");

            entity.ToTable("TradeAgreement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgreementCreator)
                .HasMaxLength(100)
                .HasColumnName("agreement_creator");
            entity.Property(e => e.AgreementStatus).HasColumnName("agreement_status");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.IdBranch).HasColumnName("id_branch");
            entity.Property(e => e.IdSupplier).HasColumnName("id_supplier");
            entity.Property(e => e.MinimumQuantity).HasColumnName("minimum_quantity");
            entity.Property(e => e.Requirements).HasColumnName("requirements");
            entity.Property(e => e.ReturnPolicy).HasColumnName("return_policy");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.IdBranchNavigation).WithMany(p => p.TradeAgreements)
                .HasForeignKey(d => d.IdBranch)
                .HasConstraintName("FK__TradeAgre__id_br__540C7B00");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.TradeAgreements)
                .HasForeignKey(d => d.IdSupplier)
                .HasConstraintName("FK__TradeAgre__id_su__55009F39");
        });

        modelBuilder.Entity<TradeAgreementAttachment>(entity =>
        {
            entity.HasKey(e => e.Filename).HasName("PK__TradeAgr__ACFCEFEF97D7997E");

            entity.Property(e => e.Filename)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasColumnName("filename");
            entity.Property(e => e.IdTradeAgreement).HasColumnName("id_TradeAgreement");

            entity.HasOne(d => d.IdTradeAgreementNavigation).WithMany(p => p.TradeAgreementAttachments)
                .HasForeignKey(d => d.IdTradeAgreement)
                .HasConstraintName("FK__TradeAgre__id_Tr__55F4C372");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("Unit");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<UnregisteredUser>(entity =>
        {
            entity.ToTable("UnregisteredUser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voucher__3213E83F9F6A6A15");

            entity.ToTable("Voucher");

            entity.Property(e => e.Id)
                .HasMaxLength(6)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Condition).HasColumnName("condition");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount");
            entity.Property(e => e.DiscountPercentage).HasColumnName("discount_percentage");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Limit).HasColumnName("limit");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasMany(d => d.IdUsers).WithMany(p => p.IdVouchers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserVoucher",
                    r => r.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserVouch__id_us__56E8E7AB"),
                    l => l.HasOne<Voucher>().WithMany()
                        .HasForeignKey("IdVoucher")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserVouch__id_vo__57DD0BE4"),
                    j =>
                    {
                        j.HasKey("IdVoucher", "IdUser");
                        j.ToTable("UserVoucher");
                        j.IndexerProperty<string>("IdVoucher")
                            .HasMaxLength(6)
                            .HasColumnName("id_voucher");
                        j.IndexerProperty<string>("IdUser").HasColumnName("id_user");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
