namespace FitCore.Application.DTOs;

/// <summary>
/// DTO for creating workout classes.
/// </summary>
public class CreateWorkoutClassDto
{
    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public int Capacity { get; set; }

    public Guid TrainerId { get; set; }

    public Guid RoomId { get; set; }
}