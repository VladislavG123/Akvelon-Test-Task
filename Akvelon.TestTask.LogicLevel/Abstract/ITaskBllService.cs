using Akvelon.TestTask.LogicLevel.DTOs;

namespace Akvelon.TestTask.LogicLevel.Abstract;

public interface ITaskBllService
{
    Task Create(TaskCreationDto task);

    Task<List<TaskDto>> GetAll(Guid userId, int take = int.MaxValue, int skip = 0);

    Task<List<TaskDto>> GetAllByProjectId(Guid projectId, Guid userId, int take = int.MaxValue, int skip = 0);
    
    Task<TaskDto> GetById(Guid id, Guid userId);

    Task AttachToProject(Guid taskId, Guid? projectId);
    
    Task Edit(TaskEditDto editDto);

    Task Delete(Guid id);
}