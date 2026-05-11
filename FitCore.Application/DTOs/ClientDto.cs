namespace FitCore.Application.DTOs;

/// <summary>
/// Client response DTO.
/// </summary>
public class ClientDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public bool IsActive { get; set; }
}