using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;

namespace Akvelon.TestTask.AutoMapperProfiles;

public class TaskProfiles : Profile
{
    public TaskProfiles()
    {
        CreateMap<TaskEntity, TaskCreationDto>().ReverseMap();
        CreateMap<TaskEntity, TaskDto>().ReverseMap();
        CreateMap<TaskCreateViewModel, TaskCreationDto>().ReverseMap();
        CreateMap<TaskCreateViewModel, TaskEditDto>().ReverseMap();
        CreateMap<TaskDto, TaskViewModel>().ReverseMap();
    }
}