namespace Order.Api;

public static class DependencyInjection
{
    public static IServiceCollection AppApiServices(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}