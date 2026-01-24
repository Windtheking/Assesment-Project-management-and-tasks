using Microsoft.AspNetCore.Identity;

namespace CleanFirmeza.Infrastructure.Identity;

public class ApplicationUsser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt  { get; set; }
    public bool IsActive { get; set; } = true;
}