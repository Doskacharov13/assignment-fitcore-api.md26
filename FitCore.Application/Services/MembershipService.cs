using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for memberships.
/// </summary>
public class MembershipService : IMembershipService
{
    public async Task<MembershipDto> UpdateAsync(
    Guid id,
    UpdateMembershipDto dto)
    {
        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == id);

        if (membership == null)
        {
            throw new Exception("Membership not found.");
        }

        membership.StartDate = dto.StartDate;
        membership.EndDate = dto.EndDate;
        membership.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return new MembershipDto
        {
            Id = membership.Id,
            ClientId = membership.ClientId,
            PlanId = membership.PlanId,
            StartDate = membership.StartDate,
            EndDate = membership.EndDate,
            IsActive = membership.IsActive
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == id);

        if (membership == null)
        {
            throw new Exception("Membership not found.");
        }

        _context.Memberships.Remove(membership);

        await _context.SaveChangesAsync();
    }
    private readonly FitCoreDbContext _context;

    public MembershipService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MembershipDto>> GetAllAsync()
    {
        return await _context.Memberships
            .Select(m => new MembershipDto
            {
                Id = m.Id,
                ClientId = m.ClientId,
                PlanId = m.PlanId,
                StartDate = m.StartDate,
                EndDate = m.EndDate
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<MembershipDto>> GetActiveAsync()
    {
        return await _context.Memberships
            .Where(m => m.EndDate > DateTime.UtcNow)
            .Select(m => new MembershipDto
            {
                Id = m.Id,
                ClientId = m.ClientId,
                PlanId = m.PlanId,
                StartDate = m.StartDate,
                EndDate = m.EndDate
            })
            .ToListAsync();
    }

    public async Task<MembershipDto> CreateAsync(CreateMembershipDto dto)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == dto.ClientId);

        if (client == null)
        {
            throw new Exception("Client not found.");
        }

        if (!client.IsActive)
        {
            throw new Exception("Inactive client cannot receive membership.");
        }

        var plan = await _context.MembershipPlans
            .FirstOrDefaultAsync(p => p.Id == dto.PlanId);

        if (plan == null)
        {
            throw new Exception("Membership plan not found.");
        }

        var membership = new Membership
        {
            Id = Guid.NewGuid(),
            ClientId = dto.ClientId,
            PlanId = dto.PlanId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(plan.DurationDays)
        };

        _context.Memberships.Add(membership);

        await _context.SaveChangesAsync();

        return new MembershipDto
        {
            Id = membership.Id,
            ClientId = membership.ClientId,
            PlanId = membership.PlanId,
            StartDate = membership.StartDate,
            EndDate = membership.EndDate
        };

    }
}