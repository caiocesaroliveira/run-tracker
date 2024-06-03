using Domain.Abstractions.Data;
using Domain.Followers;
using Domain.Users;
using Infrastructure.Contexts;
using Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseInMemoryDatabase("RunTrackerDB", 
                cg => cg.EnableNullChecks()));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();
        services.AddScoped<IUserQuery, AppDbContext>();
        services.AddScoped<IUnitOfWork, AppDbContext>();


        return services;
    }
}
