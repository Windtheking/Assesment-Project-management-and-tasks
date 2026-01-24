namespace ProjectAndTaskManagement.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Draft";
    public string Priority { get; set; } = "Medium";
    public bool IsActive { get; set; } = true;
    public bool IsCompleted { get; set; } = false;
    public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}