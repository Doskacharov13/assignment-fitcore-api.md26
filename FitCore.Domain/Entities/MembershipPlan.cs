using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Subscription plan.
/// </summary>
public class MembershipPlan : BaseEntity
{
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
}