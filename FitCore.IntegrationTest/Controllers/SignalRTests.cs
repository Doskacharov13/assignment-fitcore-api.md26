using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class SignalRTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SignalRTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task NotificationHub_ShouldExist()
    {
        var response = await _client.GetAsync("/notificationHub");

        response.StatusCode.Should()
            .NotBe(HttpStatusCode.NotFound);
    }
}