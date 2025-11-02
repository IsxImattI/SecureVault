using Microsoft.EntityFrameworkCore;
using SecureVault.Core.Entities;

namespace SecureVault.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PasswordEntry> PasswordEntries { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // configure passwordeentry
        modelBuilder.Entity<PasswordEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EncryptedPassword).IsRequired();
            entity.Property(e => e.Salt).IsRequired();
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.PasswordEntries)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // configure category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.IconName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        // seed default categories
        SeedCategories(modelBuilder);
    }

    private void SeedCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "General",
                Description = "General passwords",
                IconName = "Folder",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"),
                Name = "Social Media",
                Description = "Social media accounts",
                IconName = "People",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = new Guid("33333333-3333-3333-3333-333333333333"),
                Name = "Banking",
                Description = "Banking and financial",
                IconName = "CreditCard",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = new Guid("44444444-4444-4444-4444-444444444444"),
                Name = "Email",
                Description = "Email accounts",
                IconName = "Mail",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = new Guid("55555555-5555-5555-5555-555555555555"),
                Name = "Work",
                Description = "Work-related accounts",
                IconName = "Work",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}