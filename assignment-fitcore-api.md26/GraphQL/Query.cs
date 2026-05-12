using FitCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitCore.API.GraphQL;

/// <summary>
/// GraphQL queries.
/// </summary>
public class Query
{
    /// <summary>
    /// Get all clients.
    /// </summary>
    public async Task<IEnumerable<object>> GetClients(
        [Service] FitCoreDbContext context)
    {
        return await context.Clients
            .Select(c => new
            {
                c.Id,
                c.FullName,
                c.Email,
                c.Phone,
                c.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Get all trainers.
    /// </summary>
    public async Task<IEnumerable<object>> GetTrainers(
        [Service] FitCoreDbContext context)
    {
        return await context.Trainers
            .Select(t => new
            {
                t.Id,
                t.FullName,
                t.Specialty,
                t.IsActive
            })
            .ToListAsync();
    }

    /// <summary>
    /// Get all workout classes.
    /// </summary>
    public async Task<IEnumerable<object>> GetWorkoutClasses(
        [Service] FitCoreDbContext context)
    {
        return await context.WorkoutClasses
            .Select(w => new
            {
                w.Id,
                w.Title,
                w.StartTime,
                w.Capacity,
                w.IsCancelled
            })
            .ToListAsync();
    }
}