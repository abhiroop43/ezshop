using Carter;

namespace Order.Api;

public static class DependencyInjection
{
    public static IServiceCollection AppApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}