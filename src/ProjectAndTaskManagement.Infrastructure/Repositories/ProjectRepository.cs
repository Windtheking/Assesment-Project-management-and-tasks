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

    public async Task AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
        => await _context.Projects.ToListAsync();

    public async Task<Project?> GetByIdAsync(Guid id)
        => await _context.Projects.FindAsync(id);
}