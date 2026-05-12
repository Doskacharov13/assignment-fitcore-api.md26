namespace FitCore.Application.DTOs;

/// <summary>
/// Reservation DTO.
/// </summary>
public class ReservationDto
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid WorkoutClassId { get; set; }

    public bool Attended { get; set; }

    public bool IsCancelled { get; set; }
}