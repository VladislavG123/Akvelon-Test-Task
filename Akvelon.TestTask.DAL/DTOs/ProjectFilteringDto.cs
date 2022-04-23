namespace Akvelon.TestTask.DAL.DTOs;

public class ProjectFilteringDto
{
    public DateTime StartAt { get; set; } = DateTime.MinValue;
    public DateTime EndAt { get; set; } = DateTime.MaxValue;

    public int Skip { get; set; } = 0;
    public int Take { get; set; } = int.MaxValue;

    public string ProjectName { get; set; } = "";
    public Guid UserId { get; set; }

}