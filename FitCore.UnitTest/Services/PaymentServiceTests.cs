using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Application.Services;
using FitCore.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitCore.Tests.Unit.Services;

public class PaymentServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenAmountIsInvalid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var notificationMock = new Mock<INotificationService>();

        var service = new PaymentService(
            context,
            notificationMock.Object);

        var dto = new CreatePaymentDto
        {
            ClientId = Guid.NewGuid(),
            Amount = -10
        };

        // Act
        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Amount must be greater than zero.");
    }
    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenClientDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var notificationMock = new Mock<INotificationService>();

        var service = new PaymentService(
            context,
            notificationMock.Object);

        var dto = new CreatePaymentDto
        {
            ClientId = Guid.NewGuid(),
            Amount = 50
        };

        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Client not found.");
    }

    [Fact]
    public async Task CreateAsync_ShouldCreatePayment_WhenDataValid()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var client = new FitCore.Domain.Entities.Client
        {
            FullName = "John Doe",
            Email = "john@test.com",
            Phone = "123456",
            IsActive = true
        };

        context.Clients.Add(client);

        await context.SaveChangesAsync();

        var notificationMock = new Mock<INotificationService>();

        var service = new PaymentService(
            context,
            notificationMock.Object);

        var dto = new CreatePaymentDto
        {
            ClientId = client.Id,
            Amount = 100
        };

        var result = await service.CreateAsync(dto);

        result.Should().NotBeNull();
        result.Amount.Should().Be(100);
    }
    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenTrainerNotFound()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var notificationMock = new Mock<INotificationService>();

        var service = new WorkoutClassService(
            context,
            notificationMock.Object);

        var dto = new CreateWorkoutClassDto
        {
            Title = "Yoga",
            Capacity = 10,
            TrainerId = Guid.NewGuid(),
            RoomId = Guid.NewGuid()
        };

        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Trainer not found.");
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnPayments()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        context.Payments.Add(new FitCore.Domain.Entities.Payment
        {
            Amount = 50,
            IsConfirmed = true
        });

        await context.SaveChangesAsync();

        var notificationMock = new Mock<INotificationService>();

        var service = new PaymentService(
            context,
            notificationMock.Object);

        var result = await service.GetAllAsync();

        result.Should().HaveCount(1);
    }
}