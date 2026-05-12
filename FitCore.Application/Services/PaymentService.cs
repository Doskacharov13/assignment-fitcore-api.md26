using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for payments.
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly FitCoreDbContext _context;

    public PaymentService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
    {
        return await _context.Payments
            .Select(p => new PaymentDto
            {
                Id = p.Id,
                ClientId = p.ClientId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                IsConfirmed = p.IsConfirmed
            })
            .ToListAsync();
    }

    public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
    {
        if (dto.Amount <= 0)
        {
            throw new Exception("Payment amount must be positive.");
        }

        var client = await _context.Clients.FindAsync(dto.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found.");
        }

        var payment = new Payment
        {
            ClientId = dto.ClientId,
            Amount = dto.Amount,
            PaymentDate = DateTime.UtcNow,
            IsConfirmed = true
        };

        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        return new PaymentDto
        {
            Id = payment.Id,
            ClientId = payment.ClientId,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            IsConfirmed = payment.IsConfirmed
        };
    }
}