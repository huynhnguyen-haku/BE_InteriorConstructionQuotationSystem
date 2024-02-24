using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticleType> ArticleTypes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CompletedProject> CompletedProjects { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductInProject> ProductInProjects { get; set; } = null!;
        public virtual DbSet<Quotation> Quotations { get; set; } = null!;
        public virtual DbSet<QuotationDetail> QuotationDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Style> Styles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRequest> UserRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProjectDescription)
                    .HasMaxLength(255)
                    .HasColumnName("project_description");

                entity.Property(e => e.ProjectImage)
                    .HasMaxLength(255)
                    .HasColumnName("project_image");

                entity.Property(e => e.ProjectTitle)
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

                entity.Property(e => e.QuotationDId).HasColumnName("quotation_d_id");

                entity.Property(e => e.QuotationStatus)
                    .HasMaxLength(50)
                    .HasColumnName("quotation_status");

                entity.HasOne(d => d.QuotationD)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.QuotationDId)
                    .HasConstraintName("FK__quotation__quota__48CFD27E");
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

                entity.Property(e => e.StyleId).HasColumnName("style_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.QuotationDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__quotation__produ__440B1D61");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.QuotationDetails)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__quotation__style__44FF419A");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("style");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
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

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user__role_id__164452B1");
            });

            modelBuilder.Entity<UserRequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_request");

                entity.Property(e => e.QuotationId).HasColumnName("quotation_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Quotation)
                    .WithMany()
                    .HasForeignKey(d => d.QuotationId)
                    .HasConstraintName("FK__user_requ__quota__4BAC3F29");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_requ__user___4AB81AF0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
