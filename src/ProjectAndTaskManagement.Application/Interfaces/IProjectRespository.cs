using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application.Interfaces;

public interface IProjectRespository
{
    Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize, string? priority = null);
    Task<Project?> GetByIdAsync(Guid id);
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Project project);
    Task<bool> ExistsAsync(Guid id);
}