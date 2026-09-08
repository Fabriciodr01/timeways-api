namespace TaskFlow.Domain.Entities;

public class TaskItem : Entity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public TaskStatus Status { get; set; } = TaskStatus.Todo;

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}