using FitCore.Domain.Common;

namespace FitCore.Domain.Entities;

/// <summary>
/// Represents gym visit.
/// </summary>
public class Visit : BaseEntity
{
    public Guid ClientId { get; set; }

    public Client Client { get; set; }

    public DateTime VisitDate { get; set; }
}