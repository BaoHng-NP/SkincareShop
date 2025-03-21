using System.Collections.Generic;
using  BusinessObjects.Models;

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

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SkinQuizAnswer> SkinQuizAnswers { get; set; }

    public virtual DbSet<SkinQuizQuestion> SkinQuizQuestions { get; set; }

    public virtual DbSet<SkinType> SkinTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSkinTest> UserSkinTests { get; set; }

    public virtual DbSet<UserVoucher> UserVouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F5E2D0K;uid=sa;pwd=1234567890;database=SkincareShop;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__brands__3213E83F0D4A49FB");

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
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FFD784620");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Name, "UQ__categori__72E12F1BDD3A86CC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__content__3213E83FE93D1A18");

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

            entity.HasOne(d => d.Author).WithMany(p => p.Contents)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__content__authorI__114A936A");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__discount__3213E83F4AD15901");

            entity.ToTable("discounts");

            entity.HasIndex(e => e.Code, "UQ__discount__357D4CF93624676F").IsUnique();

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
            entity.Property(e => e.RequiredPoints).HasColumnName("requiredPoints");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__3213E83FD4CBCF51");

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
                .HasConstraintName("FK__feedbacks__produ__7F2BE32F");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__feedbacks__userI__7E37BEF6");
        });

        modelBuilder.Entity<LoyaltyPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__loyalty___3213E83F9167A93A");

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
                .HasConstraintName("FK__loyalty_p__userI__0C85DE4D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FB6EF4EF0");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__orders__userId__68487DD7");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_it__3213E83F1090222B");

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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FC11AE268");

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

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__products__brandI__5DCAEF64");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__5CD6CB2B");

            entity.HasMany(d => d.SkinTypes).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductSkinType",
                    r => r.HasOne<SkinType>().WithMany()
                        .HasForeignKey("SkinTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__product_s__skinT__7B5B524B"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__product_s__produ__7A672E12"),
                    j =>
                    {
                        j.HasKey("ProductId", "SkinTypeId").HasName("PK__product___7AAC08654C81A1D0");
                        j.ToTable("product_skin_types");
                        j.IndexerProperty<int>("ProductId").HasColumnName("productId");
                        j.IndexerProperty<int>("SkinTypeId").HasColumnName("skinTypeId");
                    });
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83FC2C7CCE0");

            entity.ToTable("product_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiryDate");
            entity.Property(e => e.ManufactureDate).HasColumnName("manufactureDate");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__product_d__produ__6383C8BA");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F8D0EF3FB");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "UQ__roles__B19478614EF16F1E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<SkinQuizAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skin_qui__3213E83FBB3E5DB2");

            entity.ToTable("skin_quiz_answers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.SkinTypeId).HasColumnName("skinTypeId");

            entity.HasOne(d => d.Question).WithMany(p => p.SkinQuizAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__skin_quiz__quest__71D1E811");

            entity.HasOne(d => d.SkinType).WithMany(p => p.SkinQuizAnswers)
                .HasForeignKey(d => d.SkinTypeId)
                .HasConstraintName("FK__skin_quiz__skinT__72C60C4A");
        });

        modelBuilder.Entity<SkinQuizQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skin_qui__3213E83FFF7FE531");

            entity.ToTable("skin_quiz_questions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Question).HasColumnName("question");
        });

        modelBuilder.Entity<SkinType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skin_typ__3213E83F331279DB");

            entity.ToTable("skin_types");

            entity.HasIndex(e => e.Name, "UQ__skin_typ__72E12F1B5A3C6267").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F24AA717D");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61642FDFC2CE").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__users__B43B145F42C04CFE").IsUnique();

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

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__users__roleId__534D60F1");

            entity.HasOne(d => d.SkinType).WithMany(p => p.Users)
                .HasForeignKey(d => d.SkinTypeId)
                .HasConstraintName("FK__users__skinTypeI__5165187F");
        });

        modelBuilder.Entity<UserSkinTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_ski__3213E83F1BDD76C6");

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
                .HasConstraintName("FK__user_skin__skinT__76969D2E");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkinTests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_skin__userI__75A278F5");
        });

        modelBuilder.Entity<UserVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_vou__3213E83F5B26229A");

            entity.ToTable("user_vouchers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiscountId).HasColumnName("discountId");
            entity.Property(e => e.RedeemedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("redeemedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Discount).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__user_vouc__disco__08B54D69");

            entity.HasOne(d => d.User).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_vouc__userI__07C12930");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
