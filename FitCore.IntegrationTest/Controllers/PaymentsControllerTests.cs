using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class PaymentsControllerTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PaymentsControllerTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPayments_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/payments");

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}