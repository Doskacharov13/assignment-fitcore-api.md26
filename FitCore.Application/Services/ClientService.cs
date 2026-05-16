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

    /// <summary>
    /// Get all clients.
    /// </summary>
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

    /// <summary>
    /// Create client.
    /// </summary>
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

    /// <summary>
    /// Update client.
    /// </summary>
    public async Task<ClientDto> UpdateAsync(
        Guid id,
        UpdateClientDto dto)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client == null)
        {
            throw new Exception("Client not found.");
        }

        client.FullName = dto.FullName;
        client.Email = dto.Email;
        client.Phone = dto.Phone;
        client.IsActive = dto.IsActive;

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

    /// <summary>
    /// Delete client.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client == null)
        {
            throw new Exception("Client not found.");
        }

        _context.Clients.Remove(client);

        await _context.SaveChangesAsync();
    }
}