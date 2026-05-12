using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents workout class.
/// </summary>
public class WorkoutClass : BaseEntity
{
    public string Name { get; set; }

    public DateTime StartTime { get; set; }

    public int DurationMinutes { get; set; }

    public int Capacity { get; set; }

    public bool IsCancelled { get; set; }

    public Guid TrainerId { get; set; }

    public Trainer Trainer { get; set; }

    public Guid RoomId { get; set; }

    public Room Room { get; set; }

    public ICollection<ClassReservation> Reservations { get; set; }
        = new List<ClassReservation>();
}