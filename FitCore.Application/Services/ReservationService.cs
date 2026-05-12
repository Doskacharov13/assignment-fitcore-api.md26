using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Application.Services;

/// <summary>
/// Service for reservations.
/// </summary>
public class ReservationService : IReservationService
{
    private readonly FitCoreDbContext _context;

    public ReservationService(FitCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {
        return await _context.ClassReservations
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                ClientId = r.ClientId,
                WorkoutClassId = r.WorkoutClassId,
                Attended = r.Attended,
                IsCancelled = r.IsCancelled
            })
            .ToListAsync();
    }

    public async Task<ReservationDto> CreateAsync(CreateReservationDto dto)
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
            throw new Exception("Client has no active membership.");
        }

        var workoutClass = await _context.WorkoutClasses
            .Include(w => w.Reservations)
            .FirstOrDefaultAsync(w => w.Id == dto.WorkoutClassId);

        if (workoutClass == null)
        {
            throw new Exception("Workout class not found.");
        }

        if (workoutClass.IsCancelled)
        {
            throw new Exception("Workout class is cancelled.");
        }

        var existingReservation = workoutClass.Reservations
            .Any(r => r.ClientId == dto.ClientId && !r.IsCancelled);

        if (existingReservation)
        {
            throw new Exception("Client already reserved this class.");
        }

        var activeReservations = workoutClass.Reservations
            .Count(r => !r.IsCancelled);

        if (activeReservations >= workoutClass.Capacity)
        {
            throw new Exception("Workout class is full.");
        }

        var reservation = new ClassReservation
        {
            ClientId = dto.ClientId,
            WorkoutClassId = dto.WorkoutClassId
        };

        _context.ClassReservations.Add(reservation);

        await _context.SaveChangesAsync();

        return new ReservationDto
        {
            Id = reservation.Id,
            ClientId = reservation.ClientId,
            WorkoutClassId = reservation.WorkoutClassId,
            Attended = reservation.Attended,
            IsCancelled = reservation.IsCancelled
        };
    }
}