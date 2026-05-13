using FitCore.API.Hubs;
using FitCore.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FitCore.API.Services;

/// <summary>
/// SignalR notification service.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hub;

    public NotificationService(IHubContext<NotificationHub> hub)
    {
        _hub = hub;
    }

    public async Task SendReservationCreatedAsync(object data)
    {
        await _hub.Clients.All.SendAsync("ReservationCreated", data);
    }

    public async Task SendPaymentConfirmedAsync(object data)
    {
        await _hub.Clients.All.SendAsync("PaymentConfirmed", data);
    }

    public async Task SendWorkoutCancelledAsync(object data)
    {
        await _hub.Clients.All.SendAsync("WorkoutClassCancelled", data);
    }
}