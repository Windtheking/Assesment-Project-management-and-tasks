using CleanFirmeza.Infrastructure.Identity;
using CleanFirmeza.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanFirmeza.Infrastructure.DependencyInjection;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUsser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();

        Console.WriteLine("Identity registrado");

        return services;
    }
}