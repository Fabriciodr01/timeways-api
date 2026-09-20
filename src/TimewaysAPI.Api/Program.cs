using Microsoft.OpenApi;
using TimewaysAPI.Application.Events;
using TimewaysAPI.Application.Health;
using Microsoft.EntityFrameworkCore;
using TimewaysAPI.Infrastructure.Persistence;
using TimewaysAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

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
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/api/health", (IHealthService healthService) =>
    Results.Ok(healthService.GetStatus()));

app.Run();
