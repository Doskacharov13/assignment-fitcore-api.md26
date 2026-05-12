namespace FitCore.Domain.Entities;

/// <summary>
/// Represents a gym trainer.
/// </summary>
public class Trainer
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Specialty { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<WorkoutClass> WorkoutClasses { get; set; }
    = new List<WorkoutClass>();
}