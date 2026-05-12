using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents gym room.
/// </summary>
public class Room : BaseEntity
{
    public string Name { get; set; }

    public int Capacity { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<WorkoutClass> WorkoutClasses { get; set; }
        = new List<WorkoutClass>();
}