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
    private readonly INotificationService _notificationService;

    public WorkoutClassService(
        FitCoreDbContext context,
        INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

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

    public async Task CancelAsync(Guid id)
    {
        var workoutClass = await _context.WorkoutClasses.FindAsync(id);

        if (workoutClass == null)
        {
            throw new Exception("Workout class not found.");
        }

        workoutClass.IsCancelled = true;

        await _context.SaveChangesAsync();

        await _notificationService.SendWorkoutCancelledAsync(new
        {
            workoutClass.Id,
            workoutClass.Title
        });
    }
}