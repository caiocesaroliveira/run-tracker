namespace Web.API;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        return services;
    }
}
