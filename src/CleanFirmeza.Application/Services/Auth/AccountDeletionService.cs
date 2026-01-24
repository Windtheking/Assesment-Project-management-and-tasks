using CleanFirmeza.Application.Interfaces.Auth;

namespace CleanFirmeza.Application.Services.Auth;

public class AccountDeletionService : IAccountDeletionService
{
    public Task SendDeleteCodeAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAccountAsync(string email, string password, string code)
    {
        throw new NotImplementedException();
    }
}