using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class ReservationsControllerTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ReservationsControllerTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetReservations_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/reservations");

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}