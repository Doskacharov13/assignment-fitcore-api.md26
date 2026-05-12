using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for visits.
/// </summary>
public interface IVisitService
{
    Task<IEnumerable<VisitDto>> GetAllAsync();

    Task<VisitDto> RegisterVisitAsync(CreateVisitDto dto);
}