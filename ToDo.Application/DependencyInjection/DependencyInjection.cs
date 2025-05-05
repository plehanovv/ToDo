using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Mapping;
using ToDo.Application.Services;
using ToDo.Application.Validations;
using ToDo.Application.Validations.FluentValidation;
using ToDo.Application.Validations.FluentValidation.Report;
using ToDo.Domain.Dto.Report;
using ToDo.Domain.Interfaces.Services;
using ToDo.Domain.Interfaces.Validations;

namespace ToDo.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}