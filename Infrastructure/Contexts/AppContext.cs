using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AppContext : DbContext, IUserQuery
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }

    public DbSet<User> Users { get; protected set; }

    public IQueryable<User> QueryUsers =>
           Users
           .AsNoTrackingWithIdentityResolution()
           .AsQueryable();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly<AppContext, UsersConfig>();
    }
}
