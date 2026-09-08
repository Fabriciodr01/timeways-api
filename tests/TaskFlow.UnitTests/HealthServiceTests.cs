using TaskFlow.Application.Services;
using Xunit;

namespace TaskFlow.UnitTests;

public sealed class HealthServiceTests
{
    [Fact]
    public void GetStatus_ReturnsHealthy()
    {
        var service = new HealthService();

        var result = service.GetStatus();

        Assert.Equal("Healthy", result.Status);
    }
}
