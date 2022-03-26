using Akvelon.TestTask.DAL.DTOs;

namespace Akvelon.TestTask.ViewModels;

public class ProjectFilteringViewModel
{
    public DateTime StartAt { get; set; } = DateTime.MinValue;
    public DateTime EndAt { get; set; } = DateTime.MaxValue;

    public int Skip { get; set; } = 0;
    public int Take { get; set; } = int.MaxValue;

    public string ProjectName { get; set; } = "";

    public ProjectOrdering Ordering { get; set; } = ProjectOrdering.StartDate;
    public bool Descending { get; set; } = false;
}