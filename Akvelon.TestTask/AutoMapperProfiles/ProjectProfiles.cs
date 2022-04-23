using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.DAL.DTOs;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;

namespace Akvelon.TestTask.AutoMapperProfiles;

public class ProjectProfiles : Profile
{
    public ProjectProfiles()
    {
        CreateMap<ProjectDto, ProjectViewModel>().ReverseMap();
        CreateMap<ProjectEntity, ProjectDto>().ReverseMap();
        CreateMap<ProjectDto, ProjectEntity>().ReverseMap();
        CreateMap<ProjectEditDto, ProjectEntity>().ReverseMap();
        CreateMap<ProjectCreationDto, ProjectCreationViewModel>().ReverseMap();
        CreateMap<ProjectCreationDto, ProjectEntity>().ReverseMap();
        CreateMap<ProjectFilteringViewModel, ProjectFilteringDto>().ReverseMap();
        CreateMap<ProjectEditViewModel, ProjectEditDto>().ReverseMap();
    }
}