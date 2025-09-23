using Microsoft.Extensions.DependencyInjection;
using ToDo.Producer.Interfaces;

namespace ToDo.Producer.DependencyInjection;

public static class DependencyInjection
{
    public static void AddProducer(this IServiceCollection services)
    {
        services.AddScoped<IMessageProducer, Producer>();
    }
}