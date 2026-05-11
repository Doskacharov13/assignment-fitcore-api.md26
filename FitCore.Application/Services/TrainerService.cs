using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for trainers.
/// </summary>
public class TrainerService : ITrainerService
{
    private readonly FitCoreDbContext _context;

    public TrainerService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrainerDto>> GetAllAsync()
    {
        return await _context.Trainers
            .Select(t => new TrainerDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Specialty = t.Specialty,
                IsActive = t.IsActive
            })
            .ToListAsync();
    }

    public async Task<TrainerDto> CreateAsync(CreateTrainerDto dto)
    {
        var trainer = new Trainer
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Specialty = dto.Specialty,
            IsActive = true
        };

        _context.Trainers.Add(trainer);

        await _context.SaveChangesAsync();

        return new TrainerDto
        {
            Id = trainer.Id,
            FullName = trainer.FullName,
            Specialty = trainer.Specialty,
            IsActive = trainer.IsActive
        };
    }
}