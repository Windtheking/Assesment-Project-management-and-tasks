namespace ProjectAndTaskManagement.Application.DTOs;

public class TaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Priority { get; set; } = "Low";
    public int Order { get; set; } = 0;
    public bool IsCompleted { get; set; } = false;
}