using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Application.Services;
using FitCore.Domain.Entities;
using FitCore.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitCore.Tests.Unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenClientDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var notificationMock = new Mock<INotificationService>();

        var service = new ReservationService(
            context,
            notificationMock.Object);

        var dto = new CreateReservationDto
        {
            ClientId = Guid.NewGuid(),
            WorkoutClassId = Guid.NewGuid()
        };

        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Client not found.");
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenClientIsInactive()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var client = new Client
        {
            FullName = "Test",
            Email = "test@test.com",
            Phone = "123",
            IsActive = false
        };

        context.Clients.Add(client);

        await context.SaveChangesAsync();

        var notificationMock = new Mock<INotificationService>();

        var service = new ReservationService(
            context,
            notificationMock.Object);

        var dto = new CreateReservationDto
        {
            ClientId = client.Id,
            WorkoutClassId = Guid.NewGuid()
        };

        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Client is inactive.");
    }
    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenWorkoutClassNotFound()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var client = new Client
        {
            FullName = "John",
            Email = "john@test.com",
            Phone = "123",
            IsActive = true
        };

        var membership = new Membership
        {
            Client = client,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true
        };

        context.Clients.Add(client);
        context.Memberships.Add(membership);

        await context.SaveChangesAsync();

        var notificationMock = new Mock<INotificationService>();

        var service = new ReservationService(
            context,
            notificationMock.Object);

        var dto = new CreateReservationDto
        {
            ClientId = client.Id,
            WorkoutClassId = Guid.NewGuid()
        };

        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Workout class not found.");
    }
}