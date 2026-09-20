using Microsoft.OpenApi;
using TimewaysAPI.Application.Services;
using Microsoft.EntityFrameworkCore;
using TimewaysAPI.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Timeways API",
        Version = "v1",
        Description = "Timeways calendar and event management API."
    });
});
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/health", (IHealthService healthService) =>
    Results.Ok(healthService.GetStatus()));

app.Run();
