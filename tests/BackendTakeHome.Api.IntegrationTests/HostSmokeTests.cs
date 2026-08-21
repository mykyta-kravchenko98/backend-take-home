using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BackendTakeHome.Api.IntegrationTests;

[Collection(ApiIntegrationCollection.Name)]
public sealed class HostSmokeTests(WebApplicationFactory<Program> factory)
{
    [Fact]
    public async Task Host_starts_and_serves_the_root_endpoint()
    {
        using HttpClient client = factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        Assert.Equal("BackendTakeHome API", await response.Content.ReadFromJsonAsync<string>());
    }
}
