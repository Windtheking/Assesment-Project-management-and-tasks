namespace CleanFirmeza.Application.DTOs.Auth;

public class DeleteAccountDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Code { get; set; } = null!;
}