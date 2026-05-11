namespace FitCore.Application.DTOs;

/// <summary>
/// DTO for creating client.
/// </summary>
public class CreateClientDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
}