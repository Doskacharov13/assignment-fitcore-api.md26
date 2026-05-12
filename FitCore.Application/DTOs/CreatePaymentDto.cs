namespace FitCore.Application.DTOs;

/// <summary>
/// Create payment DTO.
/// </summary>
public class CreatePaymentDto
{
    public Guid ClientId { get; set; }

    public decimal Amount { get; set; }
}