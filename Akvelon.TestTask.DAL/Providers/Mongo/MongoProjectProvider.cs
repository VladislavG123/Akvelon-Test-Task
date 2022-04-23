using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;

namespace Akvelon.TestTask.DAL.Providers.Mongo;

public class MongoProjectProvider : MongoProvider<ProjectEntity, Guid>, IProjectProvider
{
    public MongoProjectProvider(MongoDbContext context) : base(context)
    {
    }

    public async Task<List<ProjectEntity>> GetWithFiltering(ProjectFilteringDto filteringDto, 
        ProjectOrdering ordering = ProjectOrdering.StartDate,
        bool descending = false)
    {
        return await Get(x => x.UserId.Equals(filteringDto.UserId), 
            filteringDto.Take, filteringDto.Skip);
    }
}