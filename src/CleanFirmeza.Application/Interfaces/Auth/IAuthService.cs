using CleanFirmeza.Application.DTOs.Auth;

namespace CleanFirmeza.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResultDto> LoginAsync(LoginDto loginDto);
    Task<AuthResultDto> LogoutAsync(RegisterDto registerDto);
    Task<AuthResultDto> GetCurrentUser(RegisterDto registerDto);
    Task<AuthResultDto> DeleteAccountAsync(string password);
    Task<bool> LoginAsync(string email, string password, bool rememberMe);
    Task LogoutAsync();
}