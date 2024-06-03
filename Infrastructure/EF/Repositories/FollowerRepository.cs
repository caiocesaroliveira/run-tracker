using Domain.Followers;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories;
internal sealed class FollowerRepository : IFollowerRepository
{
    private readonly AppDbContext _context;

    public FollowerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsAlreadyFollowingAsync(Guid userId, Guid followedId, CancellationToken cancellationToken)
    {
        return !await _context.Followers
            .AsNoTrackingWithIdentityResolution()
            .AnyAsync(follower => follower.UserId == userId 
                               && follower.FollowerId == followedId, 
                                  cancellationToken);
    }

    public async Task AddAsync(Follower follower)
    {
        await _context.Followers.AddAsync(follower);
    }
}
