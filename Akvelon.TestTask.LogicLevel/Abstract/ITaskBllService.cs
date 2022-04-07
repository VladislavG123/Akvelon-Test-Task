using Akvelon.TestTask.LogicLevel.DTOs;

namespace Akvelon.TestTask.LogicLevel.Abstract;

public interface ITaskBllService
{
    Task Create(TaskCreationDto task);

    Task<List<TaskDto>> GetAll(int take = int.MaxValue, int skip = 0);

    Task<List<TaskDto>> GetAllByProjectId(Guid projectId, int take = int.MaxValue, int skip = 0);
    
    Task<TaskDto> GetById(Guid id);

    Task AttachToProject(Guid taskId, Guid? projectId);
    
    Task Edit(TaskEditDto editDto);

    Task Delete(Guid id);
}