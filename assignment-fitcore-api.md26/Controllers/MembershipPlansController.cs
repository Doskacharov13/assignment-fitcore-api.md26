using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assignment_fitcore_api.md26.Controllers;

/// <summary>
/// Controller for membership plans.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MembershipPlansController : ControllerBase
{
    private readonly IMembershipPlanService _service;

    public MembershipPlansController(IMembershipPlanService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all membership plans.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var plans = await _service.GetAllAsync();

        return Ok(plans);
    }

    /// <summary>
    /// Create membership plan.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateMembershipPlanDto dto)
    {
        var plan = await _service.CreateAsync(dto);

        return Ok(plan);
    }
}