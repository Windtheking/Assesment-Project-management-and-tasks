using Microsoft.Extensions.DependencyInjection;
using ProjectAndTaskManagement.Application.Interfaces;
using ProjectAndTaskManagement.Application.Services;
using ProjectAndTaskManagement.Infrastructure.Repositories;

namespace ProjectAndTaskManagement.Infrastructure.DependencyInjection;



public static class ServiceCollectionExtensions
{
     public static void AddInfrastructure(this IServiceCollection services)
    {
        
        services.AddScoped<IProjectRespository, ProjectRepository>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddScoped<TaskService>();
        services.AddScoped<ProjectService>();

    }
}
