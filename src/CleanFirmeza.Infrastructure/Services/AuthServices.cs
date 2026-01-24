using CleanFirmeza.Application.DTOs.Auth;
using CleanFirmeza.Application.Interfaces.Auth;
using CleanFirmeza.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanFirmeza.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUsser> _userManager;
    private readonly SignInManager<ApplicationUsser> _signInManager;
    // private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(
        UserManager<ApplicationUsser> userManager,
        SignInManager<ApplicationUsser> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    
    public async Task<AuthResultDto> RegisterAsync(RegisterDto registerDto)
    {
        // Crear el usuario
        var user = new ApplicationUsser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Error al crear el usuario",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        
        return new AuthResultDto
        {
            Success = true,
            Message = "Usuario registrado exitosamente",
            User = MapToUserDto(user)
        };
        
    }
 
    public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Credenciales inválidas"
            };
        }

        if (!user.IsActive)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "La cuenta está inactiva. Revisa tu correo para reactivarla."
            };
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!passwordValid)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Credenciales inválidas"
            };
        }

        // Login con cookie (MVC)
        await _signInManager.SignInAsync(user, loginDto.RememberMe);

        // JWT: aquí SOLO autenticamos, el token se genera donde tú lo manejes
        return new AuthResultDto
        {
            Success = true,
            Message = "Login exitoso",
            User = MapToUserDto(user)
        };
    }

    public Task<AuthResultDto> LogoutAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResultDto> GetCurrentUser(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResultDto> DeleteAccountAsync(string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return false;

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            password,
            rememberMe,
            lockoutOnFailure: false
        );

        return result.Succeeded;
    }




    private UserDto MapToUserDto(ApplicationUsser user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
    
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

}