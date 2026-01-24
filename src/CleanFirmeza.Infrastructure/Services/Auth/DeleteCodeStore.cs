using Microsoft.Extensions.Caching.Memory;

namespace CleanFirmeza.Infrastructure.Services.Auth;

public class DeleteCodeStore
{
    private readonly IMemoryCache _cache;

    public DeleteCodeStore(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Save(string email, string code)
    {
        _cache.Set(
            $"delete_account_{email}",
            code,
            TimeSpan.FromMinutes(2)
        );
    }

    public bool Validate(string email, string code)
    {
        if (_cache.TryGetValue($"delete_account_{email}", out string savedCode))
        {
            return savedCode == code;
        }

        return false;
    }

    public void Remove(string email)
    {
        _cache.Remove($"delete_account_{email}");
    }
}