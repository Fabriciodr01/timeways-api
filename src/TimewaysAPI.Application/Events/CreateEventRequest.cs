namespace TimewaysAPI.Application.Events;

public sealed class CreateEventRequest
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public DateTime StartAt { get; init; }
    public DateTime EndAt { get; init; }

    public bool IsAllDay { get; init; }

    public string? Location { get; init; }

    public int OwnerId { get; init; }
}