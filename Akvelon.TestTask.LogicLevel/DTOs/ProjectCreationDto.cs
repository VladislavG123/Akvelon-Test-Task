using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.LogicLevel.DTOs;

public class ProjectCreationDto
{
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    
    public string Name { get; set; }
    public ProjectStatus Status { get; set; }
    public int Priority { get; set; }
}