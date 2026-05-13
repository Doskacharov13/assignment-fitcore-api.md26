using FitCore.Application.DTOs;
using FitCore.Application.Interfaces;
using FitCore.Application.Services;
using FitCore.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitCore.Tests.Unit.Services;

public class WorkoutClassServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenCapacityInvalid()
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
            Capacity = 0
        };

        Func<Task> act = async () =>
            await service.CreateAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Capacity must be positive.");
    }
    [Fact]
    public async Task CancelAsync_ShouldThrow_WhenWorkoutClassNotFound()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var notificationMock = new Mock<INotificationService>();

        var service = new WorkoutClassService(
            context,
            notificationMock.Object);

        Func<Task> act = async () =>
            await service.CancelAsync(Guid.NewGuid());

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Workout class not found.");
    }
}