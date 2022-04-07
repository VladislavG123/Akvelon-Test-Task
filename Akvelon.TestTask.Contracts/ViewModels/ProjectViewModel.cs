using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.Contracts.ViewModels;

public class ProjectViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    
    public string Name { get; set; }
    public ProjectStatus Status { get; set; }
    public int Priority { get; set; }
}