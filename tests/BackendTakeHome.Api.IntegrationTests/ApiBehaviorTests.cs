using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BackendTakeHome.Api.IntegrationTests;

[Collection(ApiIntegrationCollection.Name)]
public sealed class ApiBehaviorTests(WebApplicationFactory<Program> factory)
{
    [Fact]
    public async Task User_and_work_item_flow_exercises_all_required_operations()
    {
        using HttpClient client = factory.CreateClient();
        string username = $"integration-{Guid.NewGuid():N}";

        using HttpResponseMessage createUserResponse = await client.PostAsJsonAsync(
            "/api/users",
            new { Username = username });

        Assert.Equal(HttpStatusCode.Created, createUserResponse.StatusCode);
        IdResponse createdUser = Assert.IsType<IdResponse>(
            await createUserResponse.Content.ReadFromJsonAsync<IdResponse>());
        Assert.NotEqual(Guid.Empty, createdUser.Id);

        using HttpResponseMessage getUserResponse = await client.GetAsync(
            $"/api/users/{createdUser.Id}");

        Assert.Equal(HttpStatusCode.OK, getUserResponse.StatusCode);
        UserResponse retrievedUser = Assert.IsType<UserResponse>(
            await getUserResponse.Content.ReadFromJsonAsync<UserResponse>());
        Assert.Equal(createdUser.Id, retrievedUser.Id);
        Assert.Equal(username, retrievedUser.Username);

        string workItemName = $"work-item-{Guid.NewGuid():N}";
        using HttpResponseMessage createWorkItemResponse = await client.PostAsJsonAsync(
            "/api/work-items",
            new { Name = workItemName, AssigneeId = createdUser.Id });

        Assert.Equal(HttpStatusCode.Created, createWorkItemResponse.StatusCode);
        IdResponse createdWorkItem = Assert.IsType<IdResponse>(
            await createWorkItemResponse.Content.ReadFromJsonAsync<IdResponse>());
        Assert.NotEqual(Guid.Empty, createdWorkItem.Id);

        using HttpResponseMessage listWorkItemsResponse = await client.GetAsync(
            $"/api/work-items?assigneeId={createdUser.Id}");

        Assert.Equal(HttpStatusCode.OK, listWorkItemsResponse.StatusCode);
        WorkItemResponse[] workItems = Assert.IsType<WorkItemResponse[]>(
            await listWorkItemsResponse.Content.ReadFromJsonAsync<WorkItemResponse[]>());
        WorkItemResponse workItem = Assert.Single(workItems);
        Assert.Equal(createdWorkItem.Id, workItem.Id);
        Assert.Equal(workItemName, workItem.Name);
        Assert.Equal(createdUser.Id, workItem.AssigneeId);
    }

    [Fact]
    public async Task Missing_users_return_not_found_where_required()
    {
        using HttpClient client = factory.CreateClient();
        Guid missingUserId = Guid.NewGuid();

        using HttpResponseMessage getUserResponse = await client.GetAsync(
            $"/api/users/{missingUserId}");
        Assert.Equal(HttpStatusCode.NotFound, getUserResponse.StatusCode);

        using HttpResponseMessage listWorkItemsResponse = await client.GetAsync(
            $"/api/work-items?assigneeId={missingUserId}");
        Assert.Equal(HttpStatusCode.OK, listWorkItemsResponse.StatusCode);
        WorkItemResponse[] workItems = Assert.IsType<WorkItemResponse[]>(
            await listWorkItemsResponse.Content.ReadFromJsonAsync<WorkItemResponse[]>());
        Assert.Empty(workItems);

        using HttpResponseMessage createWorkItemResponse = await client.PostAsJsonAsync(
            "/api/work-items",
            new { Name = "orphaned work item", AssigneeId = missingUserId });
        Assert.Equal(HttpStatusCode.NotFound, createWorkItemResponse.StatusCode);
    }

    [Fact]
    public async Task Invalid_transport_input_returns_bad_request()
    {
        using HttpClient client = factory.CreateClient();

        using HttpResponseMessage createUserResponse = await client.PostAsJsonAsync(
            "/api/users",
            new { Username = string.Empty });
        Assert.Equal(HttpStatusCode.BadRequest, createUserResponse.StatusCode);

        using HttpResponseMessage getUserResponse = await client.GetAsync("/api/users/not-a-guid");
        Assert.Equal(HttpStatusCode.BadRequest, getUserResponse.StatusCode);

        using HttpResponseMessage createWorkItemResponse = await client.PostAsJsonAsync(
            "/api/work-items",
            new { Name = string.Empty, AssigneeId = Guid.Empty });
        Assert.Equal(HttpStatusCode.BadRequest, createWorkItemResponse.StatusCode);

        using HttpResponseMessage listWorkItemsResponse = await client.GetAsync(
            $"/api/work-items?assigneeId={Guid.Empty}");
        Assert.Equal(HttpStatusCode.BadRequest, listWorkItemsResponse.StatusCode);
    }

    private sealed record IdResponse(Guid Id);

    private sealed record UserResponse(Guid Id, string Username);

    private sealed record WorkItemResponse(Guid Id, string Name, Guid AssigneeId);
}
