using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for visits.
/// </summary>
public class VisitService : IVisitService
{
    private readonly FitCoreDbContext _context;

    public VisitService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VisitDto>> GetAllAsync()
    {
        return await _context.Visits
            .Select(v => new VisitDto
            {
                Id = v.Id,
                ClientId = v.ClientId,
                VisitDate = v.VisitDate
            })
            .ToListAsync();
    }

    public async Task<VisitDto> RegisterVisitAsync(CreateVisitDto dto)
    {
        var client = await _context.Clients
            .Include(c => c.Memberships)
            .FirstOrDefaultAsync(c => c.Id == dto.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found.");
        }

        if (!client.IsActive)
        {
            throw new Exception("Client is inactive.");
        }

        var hasActiveMembership = client.Memberships.Any(m =>
            m.IsActive &&
            m.EndDate >= DateTime.UtcNow);

        if (!hasActiveMembership)
        {
            throw new Exception("No active membership.");
        }

        var visit = new Visit
        {
            ClientId = dto.ClientId,
            VisitDate = DateTime.UtcNow
        };

        _context.Visits.Add(visit);

        await _context.SaveChangesAsync();

        return new VisitDto
        {
            Id = visit.Id,
            ClientId = visit.ClientId,
            VisitDate = visit.VisitDate
        };
    }
}