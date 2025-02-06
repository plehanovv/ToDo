using AutoMapper;
using ToDo.Domain.Dto.User;
using ToDo.Domain.Entity;

namespace ToDo.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}