using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assignment_fitcore_api.md26.Controllers;

/// <summary>
/// Controller for memberships.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MembershipsController : ControllerBase
{
    private readonly IMembershipService _service;

    public MembershipsController(IMembershipService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all memberships.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var memberships = await _service.GetAllAsync();

        return Ok(memberships);
    }

    /// <summary>
    /// Get active memberships.
    /// </summary>
    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var memberships = await _service.GetActiveAsync();

        return Ok(memberships);
    }

    /// <summary>
    /// Create membership.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateMembershipDto dto)
    {
        var membership = await _service.CreateAsync(dto);

        return Ok(membership);
    }
    /// <summary>
    /// Update membership.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateMembershipDto dto)
    {
        var membership = await _service.UpdateAsync(id, dto);

        return Ok(membership);
    }

    /// <summary>
    /// Delete membership.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}