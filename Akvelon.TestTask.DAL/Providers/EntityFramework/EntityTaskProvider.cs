using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.DAL.Providers.EntityFramework;

public class EntityTaskProvider : EntityProvider<ApplicationContext, TaskEntity, Guid>, ITaskProvider
{
    private readonly ApplicationContext _context;

    public EntityTaskProvider(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<TaskEntity>> GetAllByProjectId(Guid? projectId, Guid userId, int take = Int32.MaxValue, int skip = 0)
    {
        return await _context.Tasks.Where(x => 
                (projectId == null ? x.ProjectId == null : x.ProjectId != null)
                && x.ProjectId.Equals(projectId) 
                && x.UserId.Equals(userId))
            .OrderByDescending(x => x.CreationDate).Skip(skip).Take(take).ToListAsync();
    }

    public async Task ChangeProject(Guid taskId, Guid? projectId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(taskId));

        if (task is null)
        {
            throw new ArgumentException("Task is not found");
        }

        if (projectId is not null)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id.Equals(projectId))
                ?? throw new ArgumentException("Project is not found");

            task.ProjectId = project.Id;
        }
        else
        {
            task.ProjectId = null;
        }

        await _context.SaveChangesAsync();
    }
}