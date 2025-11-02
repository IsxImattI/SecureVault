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
        var now = DateTime.UtcNow;

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = Guid.NewGuid(), Name = "General", Description = "General passwords", IconName = "Folder", CreatedAt = now },
            new Category { Id = Guid.NewGuid(), Name = "Social Media", Description = "Social media accounts", IconName = "People", CreatedAt = now },
            new Category { Id = Guid.NewGuid(), Name = "Banking", Description = "Banking and financial", IconName = "CreditCard", CreatedAt = now },
            new Category { Id = Guid.NewGuid(), Name = "Email", Description = "Email accounts", IconName = "Mail", CreatedAt = now },
            new Category { Id = Guid.NewGuid(), Name = "Work", Description = "Work-related accounts", IconName = "Work", CreatedAt = now }
        );
    }
}