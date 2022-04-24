using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Akvelon.TestTask.LogicLevel.Abstract;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;

namespace Akvelon.TestTask.LogicLevel;

public class ProjectBllService : IProjectBllService
{
    private readonly IProjectProvider _projectProvider;
    private readonly IMapper _mapper;

    public ProjectBllService(IProjectProvider projectProvider, IMapper mapper)
    {
        _projectProvider = projectProvider;
        _mapper = mapper;
    }

    public async Task Create(ProjectCreationDto project)
    {
        await _projectProvider.Add(_mapper.Map<ProjectEntity>(project));
    }

    public async Task<List<ProjectDto>> GetAll(ProjectFilteringDto filteringDto,
        ProjectOrdering ordering = ProjectOrdering.StartDate, bool @descending = false)
    {
        return (await _projectProvider.GetWithFiltering(filteringDto, ordering, descending)).Select(
            x => _mapper.Map<ProjectDto>(x)).ToList();
    }

    public async Task<List<ProjectDto>> GetAll(int take = Int32.MaxValue, int skip = 0)
    {
        return (await _projectProvider.GetAll(take, skip))
            .Select(x => _mapper.Map<ProjectDto>(x)).ToList();
    }

    public async Task<ProjectDto> GetById(Guid id)
    {
        return _mapper.Map<ProjectDto>(await _projectProvider.GetById(id));
    }

    public async Task Edit(ProjectEditDto editDto)
    {
        var project = await _projectProvider.GetById(editDto.Id);

        if (project is null)
        {
            throw new ArgumentException($"Project with id: {editDto.Id} is not found");
        }

        project.StartDate = editDto.StartDate;
        project.CompletionDate = editDto.CompletionDate;

        project.Name = editDto.Name;
        project.Status = editDto.Status;
        project.Priority = editDto.Priority;

        await _projectProvider.Edit(project);
    }

    public async Task Delete(Guid id)
    {
        var project = await _projectProvider.GetById(id);

        if (project is null)
        {
            throw new ArgumentException($"Project with id: {id} is not found");
        }

        await _projectProvider.Remove(project);
    }
}