using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents a gym client.
/// </summary>
public class Client : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    public ICollection<ClassReservation> Reservations { get; set; }
    = new List<ClassReservation>();

    public ICollection<Visit> Visits { get; set; }
        = new List<Visit>();

    public ICollection<Payment> Payments { get; set; }
        = new List<Payment>();
}