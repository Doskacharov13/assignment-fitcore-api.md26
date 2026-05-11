using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for clients.
/// </summary>
public class ClientService : IClientService
{
    private readonly FitCoreDbContext _context;

    public ClientService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        return await _context.Clients
            .Select(c => new ClientDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                IsActive = c.IsActive
            })
            .ToListAsync();
    }

    public async Task<ClientDto> CreateAsync(CreateClientDto dto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone
        };

        _context.Clients.Add(client);

        await _context.SaveChangesAsync();

        return new ClientDto
        {
            Id = client.Id,
            FullName = client.FullName,
            Email = client.Email,
            Phone = client.Phone,
            IsActive = client.IsActive
        };
    }
}