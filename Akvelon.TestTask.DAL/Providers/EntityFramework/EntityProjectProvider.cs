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
        // Find data by filters
        var expression =
            from project in _context.Projects
            where project.StartDate >= filteringDto.StartAt &&
                  project.CompletionDate <= filteringDto.EndAt &&
                  project.Name.Contains(filteringDto.ProjectName) &&
                  project.UserId.Equals(filteringDto.UserId)
            select project;

        // Order data
        if (ordering == ProjectOrdering.Priority)
        {
            expression = descending 
                ? expression.OrderByDescending(x => x.Priority) 
                : expression.OrderBy(x => x.Priority);
        }
        else
        {
            expression = descending 
                ? expression.OrderByDescending(x => x.StartDate) 
                : expression.OrderBy(x => x.StartDate);
        }

        // Return data with pagination
        return await expression.Skip(filteringDto.Skip).Take(filteringDto.Take).ToListAsync();
    }
}