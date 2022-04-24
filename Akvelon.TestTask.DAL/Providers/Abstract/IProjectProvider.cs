using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.DAL.Providers.Abstract;

/// <summary>
/// Provides database connection for ProjectEntity with CRUD functional
/// </summary>
public interface IProjectProvider : IProvider<ProjectEntity, Guid>
{
    Task<List<ProjectEntity>> GetWithFiltering(ProjectFilteringDto filteringDto,
        ProjectOrdering ordering = ProjectOrdering.StartDate, bool descending = false);
}