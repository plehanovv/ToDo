using Serilog;
using ToDo.Api;
using ToDo.Application.DependencyInjection;
using ToDo.DAL.DependencyInjection;
using ToDo.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.AddControllers();

builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Services.AddSwagger();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo v1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "ToDo v2.0");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
