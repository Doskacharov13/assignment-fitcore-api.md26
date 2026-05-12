using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents reservation for workout class.
/// </summary>
public class ClassReservation : BaseEntity
{
    public Guid ClientId { get; set; }

    public Client Client { get; set; }

    public Guid WorkoutClassId { get; set; }

    public WorkoutClass WorkoutClass { get; set; }

    public bool IsCancelled { get; set; }

    public bool Attended { get; set; }
}