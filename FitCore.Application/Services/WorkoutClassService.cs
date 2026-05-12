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

    /// <summary>
    /// Gets all workout classes.
    /// </summary>
    public async Task<IEnumerable<WorkoutClassDto>> GetAllAsync()
    {
        return await _context.WorkoutClasses
            .Include(w => w.Trainer)
            .Include(w => w.Room)
            .Select(w => new WorkoutClassDto
            {
                Id = w.Id,
                Title = w.Title,
                StartTime = w.StartTime,
                Capacity = w.Capacity,
                IsCancelled = w.IsCancelled,
                TrainerId = w.TrainerId,
                TrainerName = w.Trainer.FullName,
                RoomId = w.RoomId,
                RoomName = w.Room.Name
            })
            .ToListAsync();
    }

    /// <summary>
    /// Gets workout class by id.
    /// </summary>
    public async Task<WorkoutClassDto?> GetByIdAsync(Guid id)
    {
        return await _context.WorkoutClasses
            .Include(w => w.Trainer)
            .Include(w => w.Room)
            .Where(w => w.Id == id)
            .Select(w => new WorkoutClassDto
            {
                Id = w.Id,
                Title = w.Title,
                StartTime = w.StartTime,
                Capacity = w.Capacity,
                IsCancelled = w.IsCancelled,
                TrainerId = w.TrainerId,
                TrainerName = w.Trainer.FullName,
                RoomId = w.RoomId,
                RoomName = w.Room.Name
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a workout class.
    /// </summary>
    public async Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto)
    {
        if (dto.Capacity <= 0)
        {
            throw new Exception("Capacity must be positive.");
        }

        var trainer = await _context.Trainers.FindAsync(dto.TrainerId);

        if (trainer == null)
        {
            throw new Exception("Trainer not found.");
        }

        if (!trainer.IsActive)
        {
            throw new Exception("Trainer is inactive.");
        }

        var room = await _context.Rooms.FindAsync(dto.RoomId);

        if (room == null)
        {
            throw new Exception("Room not found.");
        }

        var trainerConflict = await _context.WorkoutClasses.AnyAsync(w =>
            w.TrainerId == dto.TrainerId &&
            w.StartTime == dto.StartTime &&
            !w.IsCancelled);

        if (trainerConflict)
        {
            throw new Exception("Trainer already has a class at this time.");
        }

        var roomConflict = await _context.WorkoutClasses.AnyAsync(w =>
            w.RoomId == dto.RoomId &&
            w.StartTime == dto.StartTime &&
            !w.IsCancelled);

        if (roomConflict)
        {
            throw new Exception("Room already has a class at this time.");
        }

        var workoutClass = new WorkoutClass
        {
            Title = dto.Title,
            StartTime = dto.StartTime,
            Capacity = dto.Capacity,
            TrainerId = dto.TrainerId,
            RoomId = dto.RoomId,
            IsCancelled = false
        };

        _context.WorkoutClasses.Add(workoutClass);

        await _context.SaveChangesAsync();

        return new WorkoutClassDto
        {
            Id = workoutClass.Id,
            Title = workoutClass.Title,
            StartTime = workoutClass.StartTime,
            Capacity = workoutClass.Capacity,
            IsCancelled = workoutClass.IsCancelled,
            TrainerId = workoutClass.TrainerId,
            TrainerName = trainer.FullName,
            RoomId = workoutClass.RoomId,
            RoomName = room.Name
        };
    }

    /// <summary>
    /// Cancels workout class.
    /// </summary>
    public async Task CancelAsync(Guid id)
    {
        var workoutClass = await _context.WorkoutClasses.FindAsync(id);

        if (workoutClass == null)
        {
            throw new Exception("Workout class not found.");
        }

        workoutClass.IsCancelled = true;

        await _context.SaveChangesAsync();
    }
}