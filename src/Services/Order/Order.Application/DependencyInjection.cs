using BuildingBlocks.Behaviors;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Env.TraversePath().Load();
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = Env.GetString("MEDIATR_LICENSE_KEY");
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}