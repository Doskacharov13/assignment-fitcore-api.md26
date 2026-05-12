using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents a workout class.
/// </summary>
public class WorkoutClass : BaseEntity
{
    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public int Capacity { get; set; }

    public bool IsCancelled { get; set; }

    public Guid TrainerId { get; set; }

    public Trainer Trainer { get; set; }

    public Guid RoomId { get; set; }

    public Room Room { get; set; }

    public ICollection<ClassReservation> Reservations { get; set; }
        = new List<ClassReservation>();
}