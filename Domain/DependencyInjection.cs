using Domain.Followers;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Domain;
public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IFollowerService, FollowerService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
