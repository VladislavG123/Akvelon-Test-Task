namespace Akvelon.TestTask.Contracts.ViewModels;

public class TaskCreateViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public Akvelon.TestTask.DAL.Models.TaskStatus Status { get; set; }
}