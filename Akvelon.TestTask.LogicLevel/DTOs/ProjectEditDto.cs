using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.LogicLevel.DTOs;

public class ProjectEditDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    
    public string Name { get; set; }
    public ProjectStatus Status { get; set; }
    public int Priority { get; set; }   
}