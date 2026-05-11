namespace FitCore.Application.DTOs;

/// <summary>
/// Trainer response DTO.
/// </summary>
public class TrainerDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Specialty { get; set; }

    public bool IsActive { get; set; }
}