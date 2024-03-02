using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SWP391API.Models
{
    public partial class InteriorConstructionQuotationSystemContext : DbContext
    {
        public InteriorConstructionQuotationSystemContext()
        {
        }

        public InteriorConstructionQuotationSystemContext(DbContextOptions<InteriorConstructionQuotationSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleType> ArticleTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CompletedProject> CompletedProjects { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInProject> ProductInProjects { get; set; }
        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<QuotationDetail> QuotationDetails { get; set; }
        public virtual DbSet<QuotationTemp> QuotationTemps { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Style> Styles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());


        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config.GetConnectionString("DefaultConnection");

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.ArticleId).HasColumnName("article_id");

                entity.Property(e => e.ArticleTypeId).HasColumnName("article_type_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ArticleType)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.ArticleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__article__article__1CF15040");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__article__user_id__1BFD2C07");
            });

            modelBuilder.Entity<ArticleType>(entity =>
            {
                entity.ToTable("article_types");

                entity.Property(e => e.ArticleTypeId).HasColumnName("article_type_id");

                entity.Property(e => e.ArticleTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("article_type_name");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CompletedProject>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("PK__complete__BC799E1F20221A3E");

                entity.ToTable("completedProject");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");


                entity.Property(e => e.ProjectDescription)
                    .HasMaxLength(255)
                    .HasColumnName("project_description");

                entity.Property(e => e.ProjectImage)
                    .HasMaxLength(255)
                    .HasColumnName("project_image");

                entity.Property(e => e.ProjectTitle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("project_title");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.StyleId).HasColumnName("style_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.CompletedProjects)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__completed__style__5629CD9C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompletedProjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__completed__user___571DF1D5");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contract");

                entity.Property(e => e.ContractId).HasColumnName("contract_id");

                entity.Property(e => e.ContractStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("contract_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.QuotationId).HasColumnName("quotation_id");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.QuotationId)
                    .HasConstraintName("FK__contract__quotat__4F7CD00D");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("image_url");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Size)
                    .HasMaxLength(255)
                    .HasColumnName("size");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__product__categor__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__product__user_id__3D5E1FD2");
            });

            modelBuilder.Entity<ProductInProject>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ProjectId });

                entity.ToTable("ProductInProject");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInProjects)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInProject_product");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProductInProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInProject_completedProject");
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.ToTable("quotation");

                entity.Property(e => e.QuotationId).HasColumnName("quotation_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.QuotationStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("quotation_status");

                entity.Property(e => e.Square).HasColumnName("square");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StyleId).HasColumnName("style_id");

                entity.Property(e => e.TotalBill).HasColumnName("totalBill");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.StyleId)
                    .HasConstraintName("FK_quotation_style");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_quotation_user");
            });

            modelBuilder.Entity<QuotationDetail>(entity =>
            {
                entity.HasKey(e => e.QuotationDId)
                    .HasName("PK__quotatio__F90A1A865964317E");

                entity.ToTable("quotation_detail");

                entity.Property(e => e.QuotationDId).HasColumnName("quotation_d_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.QuotationId).HasColumnName("quotation_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.QuotationDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__quotation__produ__440B1D61");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.QuotationDetails)
                    .HasForeignKey(d => d.QuotationId)
                    .HasConstraintName("FK_quotation_detail_quotation");
            });

            modelBuilder.Entity<QuotationTemp>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId });

                entity.ToTable("quotation_temp");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.QuotationTemps)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_quotation_temp_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuotationTemps)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_quotation_temp_user");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("style");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AvtUrl)
                    .HasMaxLength(255)
                    .HasColumnName("avt_url");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("date")
                    .HasColumnName("birthdate");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expireDate");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Token)
                    .HasMaxLength(3000)
                    .HasColumnName("token");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user__role_id__164452B1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
