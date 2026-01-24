using CleanFirmeza.Application.Interfaces;
using CleanFirmeza.Application.Interfaces.Auth;
using CleanFirmeza.Application.Interfaces.Import;
using CleanFirmeza.Application.Services.Auth;
using CleanFirmeza.Application.Services.import;
using CleanFirmeza.Domain.Interface;
using CleanFirmeza.Infrastructure.Repositories;
using CleanFirmeza.Infrastructure.Services;
using CleanFirmeza.Infrastructure.Services.Auth;
using CleanFirmeza.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace CleanFirmeza.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductExcelMapper, ProductExcelMapper>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<DeleteCodeStore>();
        services.AddScoped<IAccountDeletionService, AccountDeletionService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddMemoryCache();


        return services;
    }
}