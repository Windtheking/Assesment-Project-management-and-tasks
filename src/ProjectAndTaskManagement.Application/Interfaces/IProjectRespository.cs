using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application.Interfaces;

public interface IProjectRespository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task AddAsync(Project project);
}