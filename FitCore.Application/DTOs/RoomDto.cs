namespace FitCore.Application.DTOs;

/// <summary>
/// Room DTO.
/// </summary>
public class RoomDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Capacity { get; set; }

    public bool IsActive { get; set; }
}