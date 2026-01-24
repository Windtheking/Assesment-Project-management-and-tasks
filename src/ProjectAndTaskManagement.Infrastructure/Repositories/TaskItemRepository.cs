using Microsoft.EntityFrameworkCore;
using ProjectAndTaskManagement.Application.Interfaces;
using ProjectAndTaskManagement.Domain.Entities;
using ProjectAndTaskManagement.Infrastructure.Persistence;

namespace ProjectAndTaskManagement.Infrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly ApplicationDbContext _context;

    public TaskItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem taskItem)
    {
        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId) => await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
}