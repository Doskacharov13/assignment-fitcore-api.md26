using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for workout classes.
/// </summary>
public class WorkoutClassService : IWorkoutClassService
{
    private readonly FitCoreDbContext _context;

    public WorkoutClassService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkoutClassDto>> GetAllAsync()
    {
        return await _context.WorkoutClasses
            .Include(w => w.Trainer)
            .Include(w => w.Room)
            .Select(w => new WorkoutClassDto
            {
                Id = w.Id,
                Name = w.Name,
                StartTime = w.StartTime,
                DurationMinutes = w.DurationMinutes,
                Capacity = w.Capacity,
                IsCancelled = w.IsCancelled,
                TrainerName = w.Trainer.FullName,
                RoomName = w.Room.Name
            })
            .ToListAsync();
    }

    public async Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto)
    {
        if (dto.Capacity <= 0)
        {
            throw new Exception("Capacity must be positive.");
        }

        var trainer = await _context.Trainers.FindAsync(dto.TrainerId);

        if (trainer == null || !trainer.IsActive)
        {
            throw new Exception("Trainer is invalid.");
        }

        var room = await _context.Rooms.FindAsync(dto.RoomId);

        if (room == null || !room.IsActive)
        {
            throw new Exception("Room is invalid.");
        }

        var classEnd = dto.StartTime.AddMinutes(dto.DurationMinutes);

        // Trainer overlap
        var trainerConflict = await _context.WorkoutClasses.AnyAsync(w =>
            w.TrainerId == dto.TrainerId &&
            !w.IsCancelled &&
            dto.StartTime < w.StartTime.AddMinutes(w.DurationMinutes) &&
            classEnd > w.StartTime);

        if (trainerConflict)
        {
            throw new Exception("Trainer already has another class.");
        }

        // Room overlap
        var roomConflict = await _context.WorkoutClasses.AnyAsync(w =>
            w.RoomId == dto.RoomId &&
            !w.IsCancelled &&
            dto.StartTime < w.StartTime.AddMinutes(w.DurationMinutes) &&
            classEnd > w.StartTime);

        if (roomConflict)
        {
            throw new Exception("Room already has another class.");
        }

        var workoutClass = new WorkoutClass
        {
            Name = dto.Name,
            StartTime = dto.StartTime,
            DurationMinutes = dto.DurationMinutes,
            Capacity = dto.Capacity,
            TrainerId = dto.TrainerId,
            RoomId = dto.RoomId
        };

        _context.WorkoutClasses.Add(workoutClass);

        await _context.SaveChangesAsync();

        return new WorkoutClassDto
        {
            Id = workoutClass.Id,
            Name = workoutClass.Name,
            StartTime = workoutClass.StartTime,
            DurationMinutes = workoutClass.DurationMinutes,
            Capacity = workoutClass.Capacity,
            IsCancelled = workoutClass.IsCancelled,
            TrainerName = trainer.FullName,
            RoomName = room.Name
        };
    }
}