namespace TimewaysAPI.Domain.Entities;

public class User : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Event> Events { get; set; } = new List<Event>();
}