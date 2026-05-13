using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;


namespace FitCore.Tests.Integration.Controllers;

public class SwaggerTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SwaggerTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Swagger_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/swagger");

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}