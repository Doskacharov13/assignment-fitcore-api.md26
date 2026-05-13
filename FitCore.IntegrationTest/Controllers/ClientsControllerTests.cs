using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class ClientsControllerTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ClientsControllerTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetClients_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/clients");

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}