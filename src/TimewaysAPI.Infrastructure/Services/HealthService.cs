using TimewaysAPI.Application.Health;

namespace TimewaysAPI.Infrastructure.Services;

public sealed class HealthService : IHealthService
{
    public HealthStatus GetStatus() => new("Healthy");
}
