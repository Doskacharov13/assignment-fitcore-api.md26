namespace FitCore.Application.DTOs;

/// <summary>
/// Visit DTO.
/// </summary>
public class VisitDto
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public DateTime VisitDate { get; set; }
}