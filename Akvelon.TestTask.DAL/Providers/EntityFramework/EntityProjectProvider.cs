using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.DAL.Providers.EntityFramework;

public class EntityProjectProvider : EntityProvider<ApplicationContext, ProjectEntity, Guid>, IProjectProvider
{
    private readonly ApplicationContext _context;

    public EntityProjectProvider(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProjectEntity>> GetWithFiltering(ProjectFilteringDto filteringDto,
        ProjectOrdering ordering = ProjectOrdering.StartDate, bool descending = false)
    {
        var expression =
            from project in _context.Projects
            where project.StartDate >= filteringDto.StartAt &&
                  project.CompletionDate <= filteringDto.EndAt &&
                  project.Name.Contains(filteringDto.ProjectName)
            select project;

        if (ordering == ProjectOrdering.Priority)
        {
            expression = descending 
                ? expression.OrderByDescending(x => x.Priority) 
                : expression.OrderBy(x => x.Priority);
        }
        else
        {
            expression = descending 
                ? expression.OrderByDescending(x => x.Name) 
                : expression.OrderBy(x => x.Name);
        }

        return await expression.Skip(filteringDto.Skip).Take(filteringDto.Take).ToListAsync();
    }
}