using Microsoft.EntityFrameworkCore;
using FitCore.Domain.Entities;

namespace FitCore.Infrastructure.Data;

/// <summary>
/// Database context for FitCore.
/// </summary>
public class FitCoreDbContext : DbContext
{
    public FitCoreDbContext(DbContextOptions<FitCoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<MembershipPlan> MembershipPlans { get; set; }
    public DbSet<Trainer> Trainers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Client → Membership (1:N)
        modelBuilder.Entity<Membership>()
            .HasOne(m => m.Client)
            .WithMany(c => c.Memberships)
            .HasForeignKey(m => m.ClientId);

        // Membership → Plan (N:1)
        modelBuilder.Entity<Membership>()
            .HasOne(m => m.Plan)
            .WithMany()
            .HasForeignKey(m => m.PlanId);
    }
}