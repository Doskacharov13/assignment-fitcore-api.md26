namespace FitCore.Application.DTOs;

/// <summary>
/// DTO for updating client.
/// </summary>
public class UpdateClientDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public bool IsActive { get; set; }
}