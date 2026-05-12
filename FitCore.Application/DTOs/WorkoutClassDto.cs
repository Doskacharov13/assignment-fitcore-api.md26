namespace FitCore.Application.DTOs;

/// <summary>
/// Workout class DTO.
/// </summary>
public class WorkoutClassDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public int Capacity { get; set; }

    public bool IsCancelled { get; set; }

    public Guid TrainerId { get; set; }

    public string TrainerName { get; set; }

    public Guid RoomId { get; set; }

    public string RoomName { get; set; }
}