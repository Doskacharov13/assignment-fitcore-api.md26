namespace FitCore.Application.DTOs;

/// <summary>
/// DTO for creating membership plan.
/// </summary>
public class CreateMembershipPlanDto
{
    public string Name { get; set; }

    public int DurationDays { get; set; }

    public decimal Price { get; set; }
}