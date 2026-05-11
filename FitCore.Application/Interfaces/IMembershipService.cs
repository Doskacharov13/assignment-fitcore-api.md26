using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Membership service contract.
/// </summary>
public interface IMembershipService
{
    Task<IEnumerable<MembershipDto>> GetAllAsync();

    Task<IEnumerable<MembershipDto>> GetActiveAsync();

    Task<MembershipDto> CreateAsync(CreateMembershipDto dto);
}