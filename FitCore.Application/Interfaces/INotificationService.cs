namespace FitCore.Application.Interfaces;

/// <summary>
/// Service for notifications.
/// </summary>
public interface INotificationService
{
    Task SendReservationCreatedAsync(object data);

    Task SendPaymentConfirmedAsync(object data);

    Task SendWorkoutCancelledAsync(object data);
}