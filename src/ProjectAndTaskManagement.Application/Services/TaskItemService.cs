using ProjectAndTaskManagement.Application.Interfaces;
using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application.Services;

public class TaskService
{
    private readonly ITaskItemRepository _repository;

    public TaskService(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene todas las tareas de un proyecto ordenadas.
    /// </summary>
    public async Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId)
    {
        return await _repository.GetAllByProjectAsync(projectId);
    }

    /// <summary>
    /// Crea una tarea asegurando que el campo Order sea único dentro del proyecto.
    /// </summary>
    public async Task AddAsync(TaskItem task)
    {
        if (task.Order == 0)
        {
            // Asignar el siguiente orden disponible automáticamente
            task.Order = await _repository.GetMaxOrderForProjectAsync(task.ProjectId) + 1;
        }
        else
        {
            // Validar que no exista duplicado
            var tasks = await _repository.GetAllByProjectAsync(task.ProjectId);
            if (tasks.Any(t => t.Order == task.Order))
                throw new InvalidOperationException("El orden de la tarea debe ser único dentro del proyecto.");
        }

        await _repository.AddAsync(task);
    }

    /// <summary>
    /// Actualiza la tarea y valida que el orden siga siendo único.
    /// </summary>
    public async Task UpdateAsync(TaskItem task)
    {
        var tasks = await _repository.GetAllByProjectAsync(task.ProjectId);
        if (tasks.Any(t => t.Id != task.Id && t.Order == task.Order))
            throw new InvalidOperationException("El orden de la tarea debe ser único dentro del proyecto.");

        await _repository.UpdateAsync(task);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Permite reordenar tareas dentro del proyecto sin duplicar el campo Order.
    /// </summary>
    public async Task ReorderAsync(Guid projectId, List<Guid> taskIdsInNewOrder)
    {
        var tasks = (await _repository.GetAllByProjectAsync(projectId)).ToList();

        if (tasks.Count != taskIdsInNewOrder.Count ||
            tasks.Select(t => t.Id).Except(taskIdsInNewOrder).Any())
        {
            throw new InvalidOperationException("Lista de tareas inválida para reordenamiento.");
        }

        for (int i = 0; i < taskIdsInNewOrder.Count; i++)
        {
            var task = tasks.First(t => t.Id == taskIdsInNewOrder[i]);
            task.Order = i + 1; // Asignar orden secuencial
            await _repository.UpdateAsync(task);
        }
    }
}