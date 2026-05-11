using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Trainer service contract.
/// </summary>
public interface ITrainerService
{
    Task<IEnumerable<TrainerDto>> GetAllAsync();

    Task<TrainerDto> CreateAsync(CreateTrainerDto dto);
}