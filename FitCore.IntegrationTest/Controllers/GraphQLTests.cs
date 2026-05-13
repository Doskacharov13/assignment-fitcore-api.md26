using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using assignment_fitcore_api.md26;

namespace FitCore.Tests.Integration.Controllers;

public class GraphQLTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GraphQLTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GraphQL_ShouldReturnSuccess()
    {
        var query = """
        {
            "query": "{ clients { id fullName } }"
        }
        """;

        var content = new StringContent(
            query,
            Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(
            "/graphql",
            content);

        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);
    }
}