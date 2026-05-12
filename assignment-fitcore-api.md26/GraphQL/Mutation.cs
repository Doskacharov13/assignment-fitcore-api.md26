using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;

namespace FitCore.API.GraphQL;

/// <summary>
/// GraphQL mutations.
/// </summary>
public class Mutation
{
    /// <summary>
    /// Create client.
    /// </summary>
    public async Task<Client> CreateClient(
        string fullName,
        string email,
        string phone,
        [Service] FitCoreDbContext context)
    {
        var client = new Client
        {
            FullName = fullName,
            Email = email,
            Phone = phone,
            IsActive = true
        };

        context.Clients.Add(client);

        await context.SaveChangesAsync();

        return client;
    }

    /// <summary>
    /// Create trainer.
    /// </summary>
    public async Task<Trainer> CreateTrainer(
        string fullName,
        string specialty,
        [Service] FitCoreDbContext context)
    {
        var trainer = new Trainer
        {
            FullName = fullName,
            Specialty = specialty,
            IsActive = true
        };

        context.Trainers.Add(trainer);

        await context.SaveChangesAsync();

        return trainer;
    }
}