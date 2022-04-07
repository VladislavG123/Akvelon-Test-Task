namespace Akvelon.TestTask.Contracts.ViewModels;

public class TaskViewModel
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
}