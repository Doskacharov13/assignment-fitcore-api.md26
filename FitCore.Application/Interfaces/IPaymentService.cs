using FitCore.Application.DTOs;

namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for payments.
/// </summary>
public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllAsync();

    Task<PaymentDto> CreateAsync(CreatePaymentDto dto);
}