using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Application;

public class ProjectDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Draft";
    public string Priority { get; set; } = "Medium";
    public bool IsActive { get; set; } = true;
    public bool IsCompleted { get; set; } = false;
    public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}