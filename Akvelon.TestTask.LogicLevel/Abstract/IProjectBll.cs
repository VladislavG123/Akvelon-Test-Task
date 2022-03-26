using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.LogicLevel.DTOs;

namespace Akvelon.TestTask.LogicLevel.Abstract;

public interface IProjectBll
{
    Task Create(ProjectCreationDto project);

    Task<List<ProjectDto>> GetAll(ProjectFilteringDto filteringDto,
        ProjectOrdering ordering = ProjectOrdering.StartDate, bool descending = false);

    Task<ProjectDto> GetById(Guid id);

    Task Edit(ProjectEditDto editDto);

    Task Delete(Guid id);
}
