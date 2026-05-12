using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitCore.API.Controllers;

/// <summary>
/// Controller for rooms.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    /// <summary>
    /// Get all rooms.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomService.GetAllAsync();

        return Ok(rooms);
    }

    /// <summary>
    /// Create room.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto dto)
    {
        var room = await _roomService.CreateAsync(dto);

        return Ok(room);
    }
}