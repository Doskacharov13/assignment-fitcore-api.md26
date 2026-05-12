using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for rooms.
/// </summary>
public class RoomService : IRoomService
{
    private readonly FitCoreDbContext _context;

    public RoomService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        return await _context.Rooms
            .Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                IsActive = r.IsActive
            })
            .ToListAsync();
    }

    public async Task<RoomDto> CreateAsync(CreateRoomDto dto)
    {
        if (dto.Capacity <= 0)
        {
            throw new Exception("Capacity must be positive.");
        }

        var room = new Room
        {
            Name = dto.Name,
            Capacity = dto.Capacity
        };

        _context.Rooms.Add(room);

        await _context.SaveChangesAsync();

        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            IsActive = room.IsActive
        };
    }
}