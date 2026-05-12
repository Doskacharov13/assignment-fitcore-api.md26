namespace FitCore.Application.DTOs;

/// <summary>
/// Create reservation DTO.
/// </summary>
public class CreateReservationDto
{
    public Guid ClientId { get; set; }

    public Guid WorkoutClassId { get; set; }
}