using Domain.Abstractions.Data;
using Domain.Followers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AppDbContext : DbContext, IUserQuery, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; protected set; }
    public DbSet<Follower> Followers { get; protected set; }

    public IQueryable<User> QueryUsers =>
           Users
           .AsNoTrackingWithIdentityResolution()
           .AsQueryable();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly<AppContext, UsersConfig>();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
