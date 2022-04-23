using Akvelon.TestTask.Contracts.ViewModels;

namespace Akvelon.TestTask.Web.Data.ComponentOptions
{
    public class ProjectComponentOption : ComponentOption<ProjectCreationViewModel>
    {
        public ProjectComponentOption()
        {
            Context = new ProjectCreationViewModel();
        }
    }
}