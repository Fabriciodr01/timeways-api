using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Microsoft.Extensions.DependencyInjection;

namespace TimewaysAPI.UnitTests;

public sealed class TimewaysApiFactory : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _postgres =
        new PostgreSqlBuilder("postgres:17-alpine")
            .WithDatabase("timeways_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _postgres.StartAsync().GetAwaiter().GetResult();

        builder.UseEnvironment("Testing");

        builder.UseSetting(
            "ConnectionStrings:DefaultConnection",
            _postgres.GetConnectionString());

        builder.UseSetting(
            "Jwt:Issuer",
            "TimewaysAPI.Tests");

        builder.UseSetting(
            "Jwt:Audience",
            "TimewaysAPI.Tests");

        builder.UseSetting(
            "Jwt:SecretKey",
            "test-secret-key-with-more-than-32-characters");

        builder.UseSetting(
            "Jwt:ExpirationMinutes",
            "60");

        builder.ConfigureServices(services =>
        {
            services.AddHostedService<TestDatabaseInitializer>();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _postgres.DisposeAsync()
                .AsTask()
                .GetAwaiter()
                .GetResult();
        }

        base.Dispose(disposing);
    }
}