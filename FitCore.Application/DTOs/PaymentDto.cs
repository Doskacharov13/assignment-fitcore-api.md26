namespace FitCore.Application.DTOs;

/// <summary>
/// Payment DTO.
/// </summary>
public class PaymentDto
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool IsConfirmed { get; set; }
}