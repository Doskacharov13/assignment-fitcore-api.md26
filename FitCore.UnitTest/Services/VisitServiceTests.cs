using FitCore.Application.DTOs;
using FitCore.Application.Services;
using FitCore.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Tests.Unit.Services;

public class VisitServiceTests
{
    [Fact]
    public async Task RegisterVisitAsync_ShouldThrow_WhenClientNotFound()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var service = new VisitService(context);

        var dto = new CreateVisitDto
        {
            ClientId = Guid.NewGuid()
        };

        Func<Task> act = async () =>
            await service.RegisterVisitAsync(dto);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Client not found.");
    }
    [Fact]
    public async Task RegisterVisitAsync_ShouldCreateVisit_WhenClientExists()
    {
        var options = new DbContextOptionsBuilder<FitCoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FitCoreDbContext(options);

        var client = new FitCore.Domain.Entities.Client
        {
            FullName = "Test",
            Email = "test@test.com",
            Phone = "123",
            IsActive = true
        };

        context.Clients.Add(client);

        await context.SaveChangesAsync();

        var service = new VisitService(context);

        var dto = new CreateVisitDto
        {
            ClientId = client.Id
        };

        var result = await service.RegisterVisitAsync(dto);

        result.Should().NotBeNull();
        result.ClientId.Should().Be(client.Id);
    }
}