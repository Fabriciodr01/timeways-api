namespace TimewaysAPI.Application.Health;

public interface IHealthService
{
    HealthStatus GetStatus();
}

public sealed record HealthStatus(string Status);
