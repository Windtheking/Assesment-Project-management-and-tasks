namespace CleanFirmeza.Application.Interfaces.Auth;

public interface IAccountDeletionService
{
    Task SendDeleteCodeAsync(string email);
    Task DeleteAccountAsync(string email, string password, string code);
}