using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;

namespace Akvelon.TestTask.LogicLevel;

public class TaskBllService : ITaskBllService 
{
    private readonly ITaskProvider _taskProvider;
    private readonly IProjectProvider _projectProvider;
    private readonly IMapper _mapper;

    public TaskBllService(ITaskProvider taskProvider, IProjectProvider projectProvider, IMapper mapper)
    {
        _taskProvider = taskProvider;
        _projectProvider = projectProvider;
        _mapper = mapper;
    }
    
    public async Task Create(TaskCreationDto task)
    {
        await _taskProvider.Add(_mapper.Map<TaskEntity>(task));
    }

    public async Task<List<TaskDto>> GetAll(int take = int.MaxValue, int skip = 0)
    {
        var result = await _taskProvider.GetAll(take, skip);

        return result.Select(x => _mapper.Map<TaskDto>(x)).ToList();
    }

    public async Task<List<TaskDto>> GetAllByProjectId(Guid projectId, int take = int.MaxValue, int skip = 0)
    {
        var result = await _taskProvider.GetAllByProjectId(projectId, take, skip);

        return result.Select(x => _mapper.Map<TaskDto>(x)).ToList();
    }
    
    public async Task<TaskDto> GetById(Guid id)
    {
        return _mapper.Map<TaskDto>(
            await _taskProvider.GetById(id) 
            ?? throw new ArgumentException($"Task with id: {id} is not found"));
    }

    public async Task AttachToProject(Guid taskId, Guid? projectId)
    {
        var task = await _taskProvider.GetById(taskId);
        if (task is null)
        {
            throw new ArgumentException($"Task with id ${taskId} is not found");
        }

        if (projectId is not null) 
        {
            var project = await _projectProvider.GetById((Guid) projectId);
            if (project is null)
            {
                throw new ArgumentException($"Project with id ${projectId} is not found");
            }
        }

        await _taskProvider.ChangeProject(taskId, projectId);
    }

    public async Task Edit(TaskEditDto editDto)
    {
        var task = await _taskProvider.GetById(editDto.Id);

        if (task is null)
        {
            throw new ArgumentException($"Task with id: {editDto.Id} is not found");
        }
        
        task.Name = editDto.Name;
        task.Description = editDto.Description;
        task.Priority = editDto.Priority;
        task.Status = editDto.Status;
        
        await _taskProvider.Edit(task);
    }

    public async Task Delete(Guid id)
    {
        var task = await _taskProvider.GetById(id);
        
        if (task is null)
        {
            throw new ArgumentException($"Task with id: {id} is not found");
        }
        
        await _taskProvider.Remove(task);
    }
}