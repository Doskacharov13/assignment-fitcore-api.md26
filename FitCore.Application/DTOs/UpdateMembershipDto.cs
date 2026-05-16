namespace FitCore.Application.DTOs;

/// <summary>
/// DTO for updating membership.
/// </summary>
public class UpdateMembershipDto
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }
}