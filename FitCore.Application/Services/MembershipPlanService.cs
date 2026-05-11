using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for membership plans.
/// </summary>
public class MembershipPlanService : IMembershipPlanService
{
    private readonly FitCoreDbContext _context;

    public MembershipPlanService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MembershipPlanDto>> GetAllAsync()
    {
        return await _context.MembershipPlans
            .Select(p => new MembershipPlanDto
            {
                Id = p.Id,
                Name = p.Name,
                DurationDays = p.DurationDays,
                Price = p.Price
            })
            .ToListAsync();
    }

    public async Task<MembershipPlanDto> CreateAsync(CreateMembershipPlanDto dto)
    {
        if (dto.Price <= 0)
        {
            throw new Exception("Price must be greater than zero.");
        }

        var plan = new MembershipPlan
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            DurationDays = dto.DurationDays,
            Price = dto.Price
        };

        _context.MembershipPlans.Add(plan);

        await _context.SaveChangesAsync();

        return new MembershipPlanDto
        {
            Id = plan.Id,
            Name = plan.Name,
            DurationDays = plan.DurationDays,
            Price = plan.Price
        };
    }
}