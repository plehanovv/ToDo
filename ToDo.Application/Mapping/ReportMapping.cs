using AutoMapper;
using ToDo.Domain.Dto.Report;
using ToDo.Domain.Entity;

namespace ToDo.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>().ReverseMap();
    }
}