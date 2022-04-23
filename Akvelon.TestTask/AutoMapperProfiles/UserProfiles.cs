using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.DAL.Models;
using Akvelon.TestTask.LogicLevel.DTOs;
using AutoMapper;

namespace Akvelon.TestTask.AutoMapperProfiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<UserEntity, UserDto>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
    }
}