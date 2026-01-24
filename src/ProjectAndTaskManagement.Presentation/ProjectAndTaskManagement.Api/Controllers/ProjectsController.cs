using Microsoft.AspNetCore.Mvc;
using ProjectAndTaskManagement.Application;
using ProjectAndTaskManagement.Application.DTOs;
using ProjectAndTaskManagement.Application.Services;
using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Api.Controllers;

[ApiController]
[Route("api/projects")]
[Produces("application/json")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _service;

    public ProjectsController(ProjectService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene la lista de proyectos, paginada y filtrable por prioridad.
    /// </summary>
    /// <param name="pageNumber">Número de página (default 1)</param>
    /// <param name="pageSize">Cantidad de proyectos por página (default 10)</param>
    /// <param name="priority">Filtra por prioridad: Low, Medium, High</param>
    /// <returns>Lista de proyectos</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? priority = null)
    {
        var projects = await _service.GetAllAsync(pageNumber, pageSize, priority);
        return Ok(projects);
    }

    /// <summary>
    /// Obtiene un proyecto específico por su Id
    /// </summary>
    /// <param name="id">Id del proyecto</param>
    /// <returns>Detalles del proyecto</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Project>> GetById(Guid id)
    {
        var project = await _service.GetByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    /// <summary>
    /// Crea un nuevo proyecto.
    /// </summary>
    /// <param name="dto">Datos del proyecto a crear</param>
    /// <remarks>
    /// Ejemplo de request:
    ///
    ///     POST /api/projects
    ///     {
    ///        "name": "Proyecto Nuevo",
    ///        "description": "Descripción detallada",
    ///        "priority": "High",
    ///        "isActive": true,
    ///        "isCompleted": false
    ///     }
    /// </remarks>
    /// <returns>Proyecto creado con Id</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Project>> Create([FromBody] ProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            Priority = dto.Priority,
            IsActive = dto.IsActive,
            IsCompleted = dto.IsCompleted
        };

        var created = await _service.CreateAsync(project);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Marca un proyecto como activo.
    /// </summary>
    /// <param name="id">Id del proyecto</param>
    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id)
    {
        var updated = await _service.MarkActiveAsync(id);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>
    /// Marca un proyecto como completado.
    /// </summary>
    /// <param name="id">Id del proyecto</param>
    [HttpPut("{id:guid}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(Guid id)
    {
        var updated = await _service.MarkCompletedAsync(id);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>
    /// Elimina un proyecto por su Id.
    /// </summary>
    /// <param name="id">Id del proyecto</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
    
    /// <summary>
    /// Devuelve un resumen del proyecto y sus tareas.
    /// </summary>
    [HttpGet("{projectId}/summary")]
    public async Task<ActionResult> GetSummary(Guid projectId)
    {
        var summary = await _service.GetSummaryAsync(projectId);
        return Ok(summary);
    }
}
