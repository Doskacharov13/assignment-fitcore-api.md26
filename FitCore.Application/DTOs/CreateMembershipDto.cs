namespace FitCore.Application.DTOs;

/// <summary>
/// DTO for creating membership.
/// </summary>
public class CreateMembershipDto
{
    public Guid ClientId { get; set; }

    public Guid PlanId { get; set; }
}