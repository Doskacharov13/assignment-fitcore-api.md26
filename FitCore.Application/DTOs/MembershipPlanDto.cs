namespace FitCore.Application.DTOs;

/// <summary>
/// Membership plan response DTO.
/// </summary>
public class MembershipPlanDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int DurationDays { get; set; }

    public decimal Price { get; set; }
}