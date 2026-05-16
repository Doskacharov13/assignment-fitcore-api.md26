using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Interface for workout class service.
/// </summary>
public interface IWorkoutClassService
{
    Task<IEnumerable<WorkoutClassDto>> GetAllAsync();

    Task<WorkoutClassDto?> GetByIdAsync(Guid id);

    Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto);

    Task<WorkoutClassDto> UpdateAsync(
        Guid id,
        CreateWorkoutClassDto dto);

    Task DeleteAsync(Guid id);

    Task CancelAsync(Guid id);
}