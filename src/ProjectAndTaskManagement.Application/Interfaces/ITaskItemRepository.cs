using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
    Task<int> GetMaxOrderForProjectAsync(Guid projectId);
}