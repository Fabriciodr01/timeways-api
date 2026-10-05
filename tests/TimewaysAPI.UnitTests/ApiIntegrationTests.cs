using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TimewaysAPI.Application.Auth;
using TimewaysAPI.Application.Events;
using Xunit;

namespace TimewaysAPI.UnitTests;

public sealed class ApiIntegrationTests : IClassFixture<TimewaysApiFactory>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(TimewaysApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/health", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Events_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/Events", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RegisterAndLogin_ReturnsAccessToken()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var email = $"user-{Guid.NewGuid()}@test.com";

        var registerResponse = await _client.PostAsJsonAsync(
            "/api/auth/register",
            new RegisterRequest
            {
                Name = "Test User",
                Email = email,
                Password = "Password123!"
            }, 
            cancellationToken);

        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var loginResponse = await _client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest
            {
                Email = email,
                Password = "Password123!"
            }, 
            cancellationToken);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var authResponse =
            await loginResponse.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken);

        Assert.NotNull(authResponse);
        Assert.False(string.IsNullOrWhiteSpace(authResponse.AccessToken));
    }

    [Fact]
    public async Task Events_WithAuthentication_ReturnsOnlyCurrentUsersEvents()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var user1 = await RegisterAndLoginAsync(
            $"user-{Guid.NewGuid()}@test.com",
            cancellationToken);

        var user2 = await RegisterAndLoginAsync(
            $"user-{Guid.NewGuid()}@test.com",
            cancellationToken);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", user1);

        var createResponse = await _client.PostAsJsonAsync(
            "/api/Events",
            new CreateEventRequest
            {
                Title = "User 1 Event",
                Description = "Private event",
                StartAt = DateTime.UtcNow.AddHours(1),
                EndAt = DateTime.UtcNow.AddHours(2),
                IsAllDay = false,
                Location = "Test"
            },
            cancellationToken);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", user2);

        var eventsResponse =
            await _client.GetAsync("/api/Events", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, eventsResponse.StatusCode);

        var events =
            await eventsResponse.Content
                .ReadFromJsonAsync<List<EventResponse>>(cancellationToken);

        Assert.NotNull(events);
        Assert.DoesNotContain(
            events,
            @event => @event.Title == "User 1 Event");
    }

    [Fact]
    public async Task EventOwnedByAnotherUser_ReturnsNotFound()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var ownerToken = await RegisterAndLoginAsync(
            $"owner-{Guid.NewGuid()}@test.com",
            cancellationToken);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", ownerToken);

        var createResponse = await _client.PostAsJsonAsync(
            "/api/Events",
            new CreateEventRequest
            {
                Title = "Private Event",
                Description = "Owner only",
                StartAt = DateTime.UtcNow.AddHours(1),
                EndAt = DateTime.UtcNow.AddHours(2),
                IsAllDay = false,
                Location = null
            }, cancellationToken);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdEvent =
            await createResponse.Content
                .ReadFromJsonAsync<EventResponse>(cancellationToken);

        Assert.NotNull(createdEvent);

        var otherUserToken = await RegisterAndLoginAsync(
            $"other-{Guid.NewGuid()}@test.com",
            cancellationToken);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                otherUserToken);

        var response =
            await _client.GetAsync(
                $"/api/Events/{createdEvent.Id}",
                cancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<string> RegisterAndLoginAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var registerResponse = await _client.PostAsJsonAsync(
            "/api/auth/register",
            new RegisterRequest
            {
                Name = "Integration Test User",
                Email = email,
                Password = "Password123!"
            },
            cancellationToken);

        registerResponse.EnsureSuccessStatusCode();

        var loginResponse = await _client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest
            {
                Email = email,
                Password = "Password123!"
            },
            cancellationToken);

        loginResponse.EnsureSuccessStatusCode();

        var authResponse =
            await loginResponse.Content
                .ReadFromJsonAsync<AuthResponse>();

        return authResponse!.AccessToken;
    }
}