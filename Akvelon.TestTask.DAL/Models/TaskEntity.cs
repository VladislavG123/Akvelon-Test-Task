using System.ComponentModel.DataAnnotations.Schema;

namespace Akvelon.TestTask.DAL.Models;

public class TaskEntity : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
    
    public Guid? ProjectId { get; set; }
    public virtual ProjectEntity Project { get; set; }
}