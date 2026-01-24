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

    public async Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId)
    {
        return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .OrderBy(t => t.Order)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.TaskItems.FindAsync(id);
    }

    public async Task AddAsync(TaskItem task)
    {
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.TaskItems.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetMaxOrderForProjectAsync(Guid projectId)
    {
        return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .MaxAsync(t => (int?)t.Order) ?? 0;
    }
}