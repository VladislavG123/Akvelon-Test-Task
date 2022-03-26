using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.DAL.Providers.Abstract;

public interface IProjectProvider : IProvider<ProjectEntity, Guid>
{
    Task<List<ProjectEntity>> GetWithFiltering(ProjectFilteringDto filteringDto,
        ProjectOrdering ordering = ProjectOrdering.StartDate, bool descending = false);
}