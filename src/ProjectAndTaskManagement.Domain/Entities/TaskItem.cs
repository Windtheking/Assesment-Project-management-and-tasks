namespace ProjectAndTaskManagement.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Priority { get; set; } = "Low";
    public int Order { get; set; } = 0;
    public bool IsCompleted { get; set; } = false;
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}