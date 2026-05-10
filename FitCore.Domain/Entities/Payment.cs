using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents a payment.
/// </summary>
public class Payment : BaseEntity
{
    public Guid ClientId { get; set; }

    public Client Client { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool IsSuccessful { get; set; }
}