using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents membership.
/// </summary>
public class Membership : BaseEntity
{
    public Guid ClientId { get; set; }

    public Client Client { get; set; }

    public Guid PlanId { get; set; }

    public MembershipPlan Plan { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; } = true;
}