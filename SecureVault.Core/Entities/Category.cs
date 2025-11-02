namespace SecureVault.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string IconName { get; set; } = "Folder";
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public ICollection<PasswordEntry> PasswordEntries { get; set; } = new List<PasswordEntry>();
}