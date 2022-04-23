using Akvelon.TestTask.DAL.Models;

namespace Akvelon.TestTask.DAL.Providers.Abstract;

public interface ITaskProvider : IProvider<TaskEntity, Guid>
{
    Task<List<TaskEntity>> GetAllByProjectId(Guid? projectId, Guid userId, int take = Int32.MaxValue, int skip = 0);

    Task ChangeProject(Guid taskId, Guid? projectId);
}