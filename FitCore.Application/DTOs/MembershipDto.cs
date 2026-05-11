namespace FitCore.Application.DTOs;

/// <summary>
/// Membership response DTO.
/// </summary>
public class MembershipDto
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid PlanId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}