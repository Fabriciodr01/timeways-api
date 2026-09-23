using TimewaysAPI.Application.Health;

namespace TimewaysAPI.Infrastructure.Health;

public sealed class HealthService : IHealthService
{
    public HealthStatus GetStatus() => new("Healthy");
}
