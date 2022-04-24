using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Akvelon.TestTask.Controllers;

[Route("/api/projects")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectBllService _projectBllService;
    private readonly IMapper _mapper;
    private readonly IUserBllService _userBllService;

    public ProjectController(IProjectBllService projectBllService, IMapper mapper, IUserBllService userBllService)
    {
        _projectBllService = projectBllService;
        _mapper = mapper;
        _userBllService = userBllService;
    }

    /// <summary>
    /// Get all projects with filtering
    /// </summary>
    /// <param name="filtering">Properties for filtering</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(ProjectFilteringViewModel filtering)
    {
        var user = await _userBllService
            .GetUserByToken(Request.Headers[HeaderNames.Authorization]
                .ToArray()[0].Replace("Bearer ", ""));

        var dto = _mapper.Map<ProjectFilteringDto>(filtering);

        dto.UserId = user.Id;

        var result = await _projectBllService
            .GetAll(dto, filtering.Ordering, filtering.Descending);

        return Ok(result.Select(x => _mapper.Map<ProjectViewModel>(x)).ToList());
    }

    /// <summary>
    /// Get Project by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _projectBllService.GetById(id);

            return Ok(_mapper.Map<ProjectViewModel>(result));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Create enw Project
    /// </summary>
    /// <param name="creationViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectCreationViewModel creationViewModel)
    {
        var user = await _userBllService
            .GetUserByToken(Request.Headers[HeaderNames.Authorization]
                .ToArray()[0].Replace("Bearer ", ""));

        var dto = _mapper.Map<ProjectCreationDto>(creationViewModel);
        dto.UserId = user.Id;

        await _projectBllService.Create(dto);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProjectEditViewModel viewModel)
    {
        var dto = _mapper.Map<ProjectEditDto>(viewModel);
        dto.Id = id;

        try
        {
            await _projectBllService.Edit(dto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _projectBllService.Delete(id);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
}