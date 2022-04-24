namespace Akvelon.TestTask.DAL.Models;

public class ProjectEntity : Entity
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public ProjectStatus Status { get; set; }
    public int Priority { get; set; }

    public virtual Guid UserId { get; set; }
}