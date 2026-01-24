using Microsoft.EntityFrameworkCore;
using ProjectAndTaskManagement.Application.Interfaces;
using ProjectAndTaskManagement.Domain.Entities;
using ProjectAndTaskManagement.Infrastructure.Persistence;

namespace ProjectAndTaskManagement.Infrastructure.Repositories;

public class ProjectRepository : IProjectRespository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
        => await _context.Projects.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize, string? priority = null)
    {
        var query = _context.Projects.AsQueryable();

        if (!string.IsNullOrEmpty(priority))
            query = query.Where(p => p.Priority == priority);

        return await query
            .OrderBy(p => p.Priority)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
        => await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
        => await _context.Projects.AnyAsync(p => p.Id == id);
}