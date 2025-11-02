namespace SecureVault.Core.Entities;

public class PasswordEntry
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public byte[] EncryptedPassword { get; set; } = Array.Empty<byte>();
    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    // foreign key and navigation
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}