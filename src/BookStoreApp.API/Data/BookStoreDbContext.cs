using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data
{
    public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
    {
        public BookStoreDbContext()
        {
        }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(300);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA71B411F9")
                    .IsUnique();

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_ToTable");
            });

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "1",
                        Name = "User",
                        NormalizedName = "USER"
                    },
                    new IdentityRole
                    {
                        Id = "2",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    }
                );

            var hasher = new PasswordHasher<ApiUser>();

            modelBuilder.Entity<ApiUser>()
                .HasData(
                    new ApiUser
                    {
                        Id = "1",
                        Email = "user@mail.com",
                        NormalizedEmail = "USER@MAIL.COM",
                        UserName = "user@mail.com",
                        NormalizedUserName = "USER@MAIL.COM",
                        FirstName = "user",
                        LastName = "user",
                        PasswordHash = hasher.HashPassword(null, "User123#")
                    },
                    new ApiUser
                    {
                        Id = "2",
                        Email = "admin@mail.com",
                        NormalizedEmail = "ADMIN@MAIL.COM",
                        UserName = "admin@mail.com",
                        NormalizedUserName = "ADMIN@MAIL.COM",
                        FirstName = "admin",
                        LastName = "admin",
                        PasswordHash = hasher.HashPassword(null, "Admin123#")
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "1",
                        UserId = "1"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "2",
                        UserId = "2"
                    }
                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
