using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitCore.API.Controllers;

/// <summary>
/// Controller for workout classes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WorkoutClassesController : ControllerBase
{
    private readonly IWorkoutClassService _service;

    public WorkoutClassesController(IWorkoutClassService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all workout classes.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>
    /// Create workout class.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutClassDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
    /// <summary>
    /// Update workout class.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        CreateWorkoutClassDto dto)
    {
        var workoutClass = await _service.UpdateAsync(id, dto);

        return Ok(workoutClass);
    }

    /// <summary>
    /// Delete workout class.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}