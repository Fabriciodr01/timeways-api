namespace TimewaysAPI.Application.Events;

public interface IEventService
{
    Task<IReadOnlyList<EventResponse>> GetAllAsync();

    Task<EventResponse?> GetByIdAsync(int id);

    Task<EventResponse> CreateAsync(CreateEventRequest request);

    Task<EventResponse?> UpdateAsync(
    int id,
    UpdateEventRequest request);

    Task<bool> DeleteAsync(int id);
}