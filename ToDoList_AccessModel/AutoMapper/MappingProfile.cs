using AutoMapper;
using ToDoList_DomainModel.Dtos.AuthModule.AuthResponseDto;
using ToDoList_DomainModel.Dtos.MissionModule.Request;
using ToDoList_DomainModel.Dtos.MissionModule.Response;
using ToDoList_DomainModel.Models;

namespace ToDoList_AccessModel.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>()
                 .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.IsConfirmed))
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Mission, MissionDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.LevelName, opt => opt.MapFrom(src => src.Level.Name))
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<AddMissionRequestDto, Mission>();

            CreateMap<Mission, AddMissionRequestDto>();

            CreateMap<Category, CategoryDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Level, LevelDto>();
        }
    }
}
