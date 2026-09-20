namespace TimewaysAPI.Application.Services;

public sealed class HealthService : IHealthService
{
    public HealthStatus GetStatus() => new("Healthy");
}
