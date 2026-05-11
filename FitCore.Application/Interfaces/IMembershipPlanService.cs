using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Membership plan service contract.
/// </summary>
public interface IMembershipPlanService
{
    Task<IEnumerable<MembershipPlanDto>> GetAllAsync();

    Task<MembershipPlanDto> CreateAsync(CreateMembershipPlanDto dto);
}