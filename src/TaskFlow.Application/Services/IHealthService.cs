namespace TaskFlow.Application.Services;

public interface IHealthService
{
    HealthStatus GetStatus();
}

public sealed record HealthStatus(string Status);
