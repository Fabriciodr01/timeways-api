using Microsoft.EntityFrameworkCore;
using TimewaysAPI.Application.Events;
using TimewaysAPI.Domain.Entities;
using TimewaysAPI.Infrastructure.Persistence;

namespace TimewaysAPI.Infrastructure.Services;

public sealed class EventService(ApplicationDbContext dbContext) : IEventService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

  public async Task<IReadOnlyList<EventResponse>> GetAllAsync()
    {
        return await _dbContext.Events
            .AsNoTracking()
            .Select(@event => new EventResponse
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                StartAt = @event.StartAt,
                EndAt = @event.EndAt,
                IsAllDay = @event.IsAllDay,
                Location = @event.Location
            })
            .ToListAsync();
    }

    public async Task<EventResponse?> GetByIdAsync(int id)
    {
        return await _dbContext.Events
            .AsNoTracking()
            .Where(@event => @event.Id == id)
            .Select(@event => new EventResponse
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                StartAt = @event.StartAt,
                EndAt = @event.EndAt,
                IsAllDay = @event.IsAllDay,
                Location = @event.Location
            })
            .FirstOrDefaultAsync();
    }

    public async Task<EventResponse> CreateAsync(CreateEventRequest request)
    {
        var @event = new Event
        {
            Title = request.Title,
            Description = request.Description,
            StartAt = request.StartAt,
            EndAt = request.EndAt,
            IsAllDay = request.IsAllDay,
            Location = request.Location,
            OwnerId = request.OwnerId
        };

        _dbContext.Events.Add(@event);

        await _dbContext.SaveChangesAsync();

        return new EventResponse
        {
            Id = @event.Id,
            Title = @event.Title,
            Description = @event.Description,
            StartAt = @event.StartAt,
            EndAt = @event.EndAt,
            IsAllDay = @event.IsAllDay,
            Location = @event.Location
        };
    }

    public async Task<EventResponse?> UpdateAsync(
    int id,
    UpdateEventRequest request)
    {
        var @event = await _dbContext.Events
            .FirstOrDefaultAsync(@event => @event.Id == id);

        if (@event is null)
        {
            return null;
        }

        @event.Title = request.Title;
        @event.Description = request.Description;
        @event.StartAt = request.StartAt;
        @event.EndAt = request.EndAt;
        @event.IsAllDay = request.IsAllDay;
        @event.Location = request.Location;

        await _dbContext.SaveChangesAsync();

        return new EventResponse
        {
            Id = @event.Id,
            Title = @event.Title,
            Description = @event.Description,
            StartAt = @event.StartAt,
            EndAt = @event.EndAt,
            IsAllDay = @event.IsAllDay,
            Location = @event.Location
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var @event = await _dbContext.Events
            .FirstOrDefaultAsync(@event => @event.Id == id);

        if (@event is null)
        {
            return false;
        }

        _dbContext.Events.Remove(@event);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}