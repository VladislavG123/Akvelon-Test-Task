using Akvelon.TestTask.Contracts.ViewModels;

namespace Akvelon.TestTask.Web.Data.ComponentOptions;

public class TaskComponentOption : ComponentOption<TaskCreateViewModel>
{
    public TaskComponentOption()
    {
        Context = new TaskCreateViewModel();
    }

    public Guid? ProjectId { get; set; }
}