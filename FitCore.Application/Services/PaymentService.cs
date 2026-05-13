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
    private readonly INotificationService _notificationService;

    public PaymentService(
        FitCoreDbContext context,
        INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
    {
        return await _context.Payments
            .Select(p => new PaymentDto
            {
                Id = p.Id,
                ClientId = p.ClientId,
                Amount = p.Amount,
                IsConfirmed = p.IsConfirmed
            })
            .ToListAsync();
    }

    public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
    {
        if (dto.Amount <= 0)
        {
            throw new Exception("Amount must be greater than zero.");
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
            IsConfirmed = true
        };

        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        await _notificationService.SendPaymentConfirmedAsync(new
        {
            payment.Id,
            payment.ClientId,
            payment.Amount
        });

        return new PaymentDto
        {
            Id = payment.Id,
            ClientId = payment.ClientId,
            Amount = payment.Amount,
            IsConfirmed = payment.IsConfirmed
        };
    }
}