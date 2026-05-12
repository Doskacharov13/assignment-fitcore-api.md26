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
    public DbSet<Room> Rooms { get; set; }

    public DbSet<WorkoutClass> WorkoutClasses { get; set; }

    public DbSet<ClassReservation> ClassReservations { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Visit> Visits { get; set; }

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

        // Trainer -> WorkoutClasses
        modelBuilder.Entity<WorkoutClass>()
            .HasOne(w => w.Trainer)
            .WithMany(t => t.WorkoutClasses)
            .HasForeignKey(w => w.TrainerId);

        // Room -> WorkoutClasses
        modelBuilder.Entity<WorkoutClass>()
            .HasOne(w => w.Room)
            .WithMany(r => r.WorkoutClasses)
            .HasForeignKey(w => w.RoomId);

        // Reservation -> Client
        modelBuilder.Entity<ClassReservation>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId);

        // Reservation -> WorkoutClass
        modelBuilder.Entity<ClassReservation>()
            .HasOne(r => r.WorkoutClass)
            .WithMany(w => w.Reservations)
            .HasForeignKey(r => r.WorkoutClassId);
    }
}