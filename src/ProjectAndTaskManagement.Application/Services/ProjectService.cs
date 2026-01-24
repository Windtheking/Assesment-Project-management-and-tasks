using ProjectAndTaskManagement.Application.Interfaces;
using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application.Services;

public class ProjectService
{
    private readonly IProjectRespository _projectRepo;
    private readonly ITaskItemRepository _taskRepo;

    public ProjectService(IProjectRespository projectRepo, ITaskItemRepository taskRepo)
    {
        _projectRepo = projectRepo;
        _taskRepo = taskRepo;
    }

    // Obtener todos los proyectos
    public async Task<IEnumerable<Project>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? priority = null)
        => await _projectRepo.GetAllAsync(pageNumber, pageSize, priority);

    // Obtener por Id
    public async Task<Project?> GetByIdAsync(Guid projectId)
        => await _projectRepo.GetByIdAsync(projectId);

    // Crear proyecto
    public async Task<Project> CreateAsync(Project project)
    {
        await _projectRepo.AddAsync(project);
        return project;
    }

    // Activar proyecto
    public async Task<bool> MarkActiveAsync(Guid projectId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId);
        if (project == null) return false;

        var tasks = await _taskRepo.GetAllByProjectAsync(projectId);
        if (!tasks.Any()) throw new InvalidOperationException("Un proyecto no puede activarse sin al menos una tarea.");

        project.IsActive = true;
        await _projectRepo.UpdateAsync(project);
        return true;
    }

    // Completar proyecto
    public async Task<bool> MarkCompletedAsync(Guid projectId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId);
        if (project == null) return false;

        var tasks = await _taskRepo.GetAllByProjectAsync(projectId);
        if (tasks.Any(t => !t.IsCompleted)) throw new InvalidOperationException("Todas las tareas deben estar completadas para cerrar el proyecto.");

        project.IsCompleted = true;
        await _projectRepo.UpdateAsync(project);
        return true;
    }

    // Eliminar proyecto
    public async Task<bool> DeleteAsync(Guid projectId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId);
        if (project == null) return false;

        await _projectRepo.DeleteAsync(project);
        return true;
    }

    // Resumen
    public async Task<object> GetSummaryAsync(Guid projectId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId) ?? throw new InvalidOperationException("Proyecto no encontrado.");
        var tasks = await _taskRepo.GetAllByProjectAsync(projectId);

        return new
        {
            ProjectId = project.Id,
            ProjectName = project.Name,
            IsActive = project.IsActive,
            IsCompleted = project.IsCompleted,
            TotalTasks = tasks.Count(),
            TotalCompletedTasks = tasks.Count(t => t.IsCompleted)
        };
    }
}
