using System.Collections.Generic;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace System.DAL;

public partial class SkincareShopContext : DbContext
{
    public SkincareShopContext()
    {
    }

    public SkincareShopContext(DbContextOptions<SkincareShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<LoyaltyPoint> LoyaltyPoints { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SkinQuizAnswer> SkinQuizAnswers { get; set; }

    public virtual DbSet<SkinQuizQuestion> SkinQuizQuestions { get; set; }

    public virtual DbSet<SkinType> SkinTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSkinTest> UserSkinTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F5E2D0K;uid=sa;pwd=1234567890;database=SkincareShop;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__brands__3213E83F7992EF06");

            entity.ToTable("brands");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(255)
                .HasColumnName("brandName");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .HasColumnName("countryCode");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F04C97D6F");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Name, "UQ__categori__72E12F1B84FA6D21").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__content__3213E83F72191B8B");

            entity.ToTable("content");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Content1).HasColumnName("content");
            entity.Property(e => e.ContentType)
                .HasMaxLength(50)
                .HasColumnName("contentType");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.ImageUrl).HasColumnName("imageUrl");
            entity.Property(e => e.IsPublished)
                .HasDefaultValue(true)
                .HasColumnName("isPublished");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("datetime")
                .HasColumnName("publishedAt");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Author).WithMany(p => p.Contents)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__content__authorI__151B244E");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__discount__3213E83F3F7A14E6");

            entity.ToTable("discounts");

            entity.HasIndex(e => e.Code, "UQ__discount__357D4CF9D1CA20A1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(50)
                .HasColumnName("discountType");
            entity.Property(e => e.DiscountValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discountValue");
            entity.Property(e => e.ExpirationDate).HasColumnName("expirationDate");
            entity.Property(e => e.MaxDiscount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("maxDiscount");
            entity.Property(e => e.MinOrderValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("minOrderValue");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__3213E83F3C5BDB74");

            entity.ToTable("feedbacks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Product).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__feedbacks__produ__06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__feedbacks__userI__05D8E0BE");
        });

        modelBuilder.Entity<LoyaltyPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__loyalty___3213E83F3A36B9CA");

            entity.ToTable("loyalty_points");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.LoyaltyPointsNavigation)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__loyalty_p__userI__10566F31");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FCD8F74DF");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalPrice");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__orders__userId__656C112C");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_it__3213E83FDBD85E86");

            entity.ToTable("order_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__order_ite__order__6C190EBB");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_ite__produ__6D0D32F4");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payments__3213E83FC11C244F");

            entity.ToTable("payments");

            entity.HasIndex(e => e.TransactionId, "UQ__payments__9B57CF73BEF82858").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("pending")
                .HasColumnName("paymentStatus");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .HasColumnName("transactionId");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__payments__orderI__70DDC3D8");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FA2FDDD48");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl).HasColumnName("imageUrl");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0.0)
                .HasColumnName("rating");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__products__brandI__5EBF139D");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__5DCAEF64");

            entity.HasMany(d => d.SkinTypes).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductSkinType",
                    r => r.HasOne<SkinType>().WithMany()
                        .HasForeignKey("SkinTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__product_s__skinT__02FC7413"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__product_s__produ__02084FDA"),
                    j =>
                    {
                        j.HasKey("ProductId", "SkinTypeId").HasName("PK__product___7AAC0865EB5442F6");
                        j.ToTable("product_skin_types");
                        j.IndexerProperty<int>("ProductId").HasColumnName("productId");
                        j.IndexerProperty<int>("SkinTypeId").HasColumnName("skinTypeId");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FC76CB0D6");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "UQ__roles__B1947861C55720EF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<SkinQuizAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skin_qui__3213E83F493D17F7");

            entity.ToTable("skin_quiz_answers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.SkinTypeId).HasColumnName("skinTypeId");

            entity.HasOne(d => d.Question).WithMany(p => p.SkinQuizAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__skin_quiz__quest__797309D9");

            entity.HasOne(d => d.SkinType).WithMany(p => p.SkinQuizAnswers)
                .HasForeignKey(d => d.SkinTypeId)
                .HasConstraintName("FK__skin_quiz__skinT__7A672E12");
        });

        modelBuilder.Entity<SkinQuizQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skin_qui__3213E83FEEE49C3F");

            entity.ToTable("skin_quiz_questions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Question).HasColumnName("question");
        });

        modelBuilder.Entity<SkinType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skin_typ__3213E83FE7DD8F4F");

            entity.ToTable("skin_types");

            entity.HasIndex(e => e.Name, "UQ__skin_typ__72E12F1B41985F4F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FA2A8F8D5");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164C7AF0D2C").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__users__B43B145F50AB96EC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("fullName");
            entity.Property(e => e.LoyaltyPoints)
                .HasDefaultValue(0)
                .HasColumnName("loyaltyPoints");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("passwordHash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.SkinTypeId).HasColumnName("skinTypeId");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__users__roleId__534D60F1");

            entity.HasOne(d => d.SkinType).WithMany(p => p.Users)
                .HasForeignKey(d => d.SkinTypeId)
                .HasConstraintName("FK__users__skinTypeI__5165187F");
        });

        modelBuilder.Entity<UserSkinTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_ski__3213E83FD6C905D8");

            entity.ToTable("user_skin_tests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.SkinTypeId).HasColumnName("skinTypeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.SkinType).WithMany(p => p.UserSkinTests)
                .HasForeignKey(d => d.SkinTypeId)
                .HasConstraintName("FK__user_skin__skinT__7E37BEF6");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkinTests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_skin__userI__7D439ABD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
