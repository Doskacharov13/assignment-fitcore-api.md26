using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for workout classes.
/// </summary>
public interface IWorkoutClassService
{
    Task<IEnumerable<WorkoutClassDto>> GetAllAsync();

    Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto);
}