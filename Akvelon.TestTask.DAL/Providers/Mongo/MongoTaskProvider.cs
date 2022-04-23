using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;

namespace Akvelon.TestTask.DAL.Providers.Mongo;

public class MongoTaskProvider : MongoProvider<TaskEntity, Guid>, ITaskProvider
{
    public MongoTaskProvider(MongoDbContext context) : base(context)
    {
    }

    public async Task<List<TaskEntity>> GetAllByProjectId(Guid? projectId, Guid userId, int take = Int32.MaxValue, int skip = 0)
    {
        return await Get(x => x.ProjectId.Equals(projectId) && x.UserId.Equals(userId), take, skip);
    }

    public async Task ChangeProject(Guid taskId, Guid? projectId)
    {
        var task = await FirstOrDefault(x => x.Id.Equals(taskId));

        if (task is null)
        {
            throw new ArgumentException("No such task");
        }

        task.ProjectId = projectId;

        await Edit(task);
    }
}