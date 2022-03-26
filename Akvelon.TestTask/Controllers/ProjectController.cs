using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using Akvelon.TestTask.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon.TestTask.Controllers;

[Route("/api/projects")]
public class ProjectController : ControllerBase
{
    private readonly IProjectBll _projectBll;
    private readonly IMapper _mapper;

    public ProjectController(IProjectBll projectBll, IMapper mapper)
    {
        _projectBll = projectBll;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all projects with filtering
    /// </summary>
    /// <param name="filtering">Properties for filtering</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(ProjectFilteringViewModel filtering)
    {
        var result = await _projectBll
            .GetAll(_mapper.Map<ProjectFilteringDto>(filtering), filtering.Ordering, filtering.Descending);

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
            var result = await _projectBll.GetById(id);

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
        await _projectBll.Create(
            _mapper.Map<ProjectCreationDto>(creationViewModel));

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id)
    {
        await Task.Run(() => { });
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Task.Run(() => { });
        return Ok();
    }
}