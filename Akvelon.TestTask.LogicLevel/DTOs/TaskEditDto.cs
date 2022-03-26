using TaskStatus = Akvelon.TestTask.DAL.Models.TaskStatus;

namespace Akvelon.TestTask.LogicLevel.DTOs;

public class TaskEditDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
}