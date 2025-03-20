using AutoMapper;
using ToDo.Domain.Dto.Role;
using ToDo.Domain.Entity;

namespace ToDo.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}