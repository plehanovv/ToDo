using System.Reflection;
using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace ToDo.Api;

public static class Startup
{
    /// <summary>
    /// Подключение Swagger
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "ToDo.Api",
                Description = "This is v1.0",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact()
                {
                    Name = "Test contact",
                    Email = "example@email.com",
                    Url = new Uri("https://example.com/url")
                }
            });

            options.SwaggerDoc("v2", new OpenApiInfo()
            {
                Version = "v2",
                Title = "ToDo.Api",
                Description = "This is v2.0",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact()
                {
                    Name = "Test contact",
                    Email = "example@email.com",
                    Url = new Uri("https://example.com/url")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }
}