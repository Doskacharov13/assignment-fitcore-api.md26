namespace FitCore.Application.DTOs;

/// <summary>
/// Workout class DTO.
/// </summary>
public class WorkoutClassDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime StartTime { get; set; }

    public int DurationMinutes { get; set; }

    public int Capacity { get; set; }

    public bool IsCancelled { get; set; }

    public string TrainerName { get; set; }

    public string RoomName { get; set; }
}