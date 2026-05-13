using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class WorkoutClassesControllerTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WorkoutClassesControllerTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWorkoutClasses_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/workoutclasses");

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}