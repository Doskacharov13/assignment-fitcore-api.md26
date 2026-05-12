using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for workout classes.
/// </summary>
public interface IWorkoutClassService
{
    /// <summary>
    /// Gets all workout classes.
    /// </summary>
    Task<IEnumerable<WorkoutClassDto>> GetAllAsync();

    /// <summary>
    /// Gets workout class by id.
    /// </summary>
    Task<WorkoutClassDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a workout class.
    /// </summary>
    Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto);

    /// <summary>
    /// Cancels a workout class.
    /// </summary>
    Task CancelAsync(Guid id);
}