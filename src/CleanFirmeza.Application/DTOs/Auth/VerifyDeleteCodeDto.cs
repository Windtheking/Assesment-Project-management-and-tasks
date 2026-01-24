namespace CleanFirmeza.Application.DTOs.Auth;

public class VerifyDeleteCodeDto
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}