using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class ClientsPostTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ClientsPostTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateClient_ShouldReturnSuccess()
    {
        var json = """
        {
            "fullName": "Integration User",
            "email": "integration@test.com",
            "phone": "123456"
        }
        """;

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(
            "/api/clients",
            content);

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}