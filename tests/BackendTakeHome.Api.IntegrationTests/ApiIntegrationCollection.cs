using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BackendTakeHome.Api.IntegrationTests;

[CollectionDefinition(Name)]
public sealed class ApiIntegrationCollection : ICollectionFixture<WebApplicationFactory<Program>>
{
    public const string Name = "API integration";
}
