using System.ComponentModel.DataAnnotations;
using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Akvelon.TestTask.Controllers;

[Route("api/tasks")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskBllService _taskBllService;
    private readonly IUserBllService _userBllService;
    private readonly IMapper _mapper;

    public TaskController(ITaskBllService taskBllService, IUserBllService userBllService, IMapper mapper)
    {
        _taskBllService = taskBllService;
        _userBllService = userBllService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all tasks
    /// </summary>
    /// <param name="projectId">Search tasks by this project id, can be null</param>
    /// <param name="take">Take N tasks, for pagination purposes</param>
    /// <param name="skip">Skip N tasks, for pagination purposes</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid? projectId = null, int take = Int32.MaxValue, int skip = 0)
    {
        var user = await _userBllService
            .GetUserByToken(Request.Headers[HeaderNames.Authorization]
                .ToArray()[0].Replace("Bearer ", ""));
        
        var result = new List<TaskDto>();
        if (projectId is null)
        {
            result.AddRange(await _taskBllService.GetAll(user.Id, take, skip));
        }
        else
        {
            result.AddRange(await _taskBllService.GetAllByProjectId((Guid) projectId, user.Id, take, skip));
        }

        return Ok(result.Select(x => _mapper.Map<TaskViewModel>(x)).ToList());
    }

    /// <summary>
    /// Get Task by unique Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userBllService
            .GetUserByToken(Request.Headers[HeaderNames.Authorization]
                .ToArray()[0].Replace("Bearer ", ""));
        
        try
        {
            var result = await _taskBllService.GetById(user.Id, id);

            return Ok(_mapper.Map<TaskDto>(result));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    /// <summary>
    /// New Task creation
    /// </summary>
    /// <param name="createViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateViewModel createViewModel)
    {
        var user = await _userBllService
            .GetUserByToken(Request.Headers[HeaderNames.Authorization]
                .ToArray()[0].Replace("Bearer ", ""));
        
        var dto = _mapper.Map<TaskCreationDto>(createViewModel);
        dto.UserId = user.Id;
        
        try
        {
            await _taskBllService.Create(dto);

            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Attaching task to project. To reattach send null as projectId
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> AttachToProject(Guid id, Guid? projectId)
    {
        try
        {
            await _taskBllService.AttachToProject(id, projectId);

            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Edit Task Info
    /// </summary>
    /// <param name="id"></param>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] TaskCreateViewModel viewModel)
    {
        var request = _mapper.Map<TaskEditDto>(viewModel);
        request.Id = id;

        try
        {
            await _taskBllService.Edit(request);

            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Delete Task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _taskBllService.Delete(id);

            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
}