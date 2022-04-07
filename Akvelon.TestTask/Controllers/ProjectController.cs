using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon.TestTask.Controllers;

[Route("/api/projects")]
public class ProjectController : ControllerBase
{
    private readonly IProjectBllService _projectBllService;
    private readonly IMapper _mapper;

    public ProjectController(IProjectBllService projectBllService, IMapper mapper)
    {
        _projectBllService = projectBllService;
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
        var result = await _projectBllService
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
        await _projectBllService.Create(
            _mapper.Map<ProjectCreationDto>(creationViewModel));

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id)
    {
        return Problem("Not implemented");
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