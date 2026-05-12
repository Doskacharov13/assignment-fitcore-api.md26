using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for reservations.
/// </summary>
public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllAsync();

    Task<ReservationDto> CreateAsync(CreateReservationDto dto);
}