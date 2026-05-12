namespace FitCore.Application.DTOs;

/// <summary>
/// Create workout class DTO.
/// </summary>
public class CreateWorkoutClassDto
{
    public string Name { get; set; }

    public DateTime StartTime { get; set; }

    public int DurationMinutes { get; set; }

    public int Capacity { get; set; }

    public Guid TrainerId { get; set; }

    public Guid RoomId { get; set; }
}