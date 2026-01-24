using Microsoft.AspNetCore.Mvc;
using ProjectAndTaskManagement.Application.Services;
using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todas las tareas asociadas a un proyecto específico.
    /// </summary>
    /// <param name="projectId">Id del proyecto del cual se desean obtener las tareas.</param>
    /// <returns>Lista de tareas del proyecto, ordenadas por su campo Order.</returns>
    /// <response code="200">Devuelve la lista de tareas del proyecto.</response>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllByProject(Guid projectId)
    {
        var tasks = await _service.GetAllByProjectAsync(projectId);
        return Ok(tasks);
    }

    /// <summary>
    /// Crea una nueva tarea dentro de un proyecto.
    /// </summary>
    /// <param name="task">Objeto TaskItem que contiene los datos de la tarea a crear.</param>
    /// <returns>La tarea creada con su Id generado.</returns>
    /// <remarks>
    /// Requisitos básicos:
    /// - `Title` no puede estar vacío.
    /// - `ProjectId` debe corresponder a un proyecto existente.
    /// - `Order` será asignado automáticamente si se deja en 0.
    /// </remarks>
    /// <response code="200">Tarea creada correctamente.</response>
    /// <response code="400">Si los datos de la tarea son inválidos.</response>
    [HttpPost]
    public async Task<ActionResult> Create(TaskItem task)
    {
        await _service.AddAsync(task);
        return Ok(task);
    }

    /// <summary>
    /// Actualiza los datos de una tarea existente.
    /// </summary>
    /// <param name="id">Id de la tarea que se desea actualizar.</param>
    /// <param name="task">Objeto TaskItem con los datos actualizados.</param>
    /// <returns>Tarea actualizada.</returns>
    /// <response code="200">Tarea actualizada correctamente.</response>
    /// <response code="400">Si el Id de la ruta no coincide con el Id del objeto.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, TaskItem task)
    {
        if (id != task.Id) return BadRequest();
        await _service.UpdateAsync(task);
        return Ok(task);
    }

    /// <summary>
    /// Elimina una tarea existente.
    /// </summary>
    /// <param name="id">Id de la tarea a eliminar.</param>
    /// <returns>NoContent si la operación fue exitosa.</returns>
    /// <response code="204">Tarea eliminada correctamente.</response>
    /// <response code="404">Si la tarea no existe.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
