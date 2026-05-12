using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for rooms.
/// </summary>
public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync();

    Task<RoomDto> CreateAsync(CreateRoomDto dto);
}