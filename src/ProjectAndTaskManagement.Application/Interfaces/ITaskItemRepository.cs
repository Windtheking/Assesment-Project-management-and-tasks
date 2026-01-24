using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application.Interfaces;

public interface ITaskItemRepository
{
    Task AddAsync(TaskItem taskItem);
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);    
}