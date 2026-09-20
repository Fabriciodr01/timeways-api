namespace TimewaysAPI.Domain.Entities;

public class Event : Entity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }

    public bool IsAllDay { get; set; }

    public string? Location { get; set; }

    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;
}