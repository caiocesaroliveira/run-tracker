using Domain.Abstractions;
using Domain.Users;

namespace Domain.Followers;
public sealed class FollowerService(IFollowerRepository repository)
{
    private readonly IFollowerRepository _followerRepository = repository;

    public async Task<Result> StartFollowing(
        User user,
        User followed,
        DateTime createdOnUtc,
        CancellationToken cancellationToken = default)
    {
        if (user.Id == followed.Id)
        {
            return FollowerErrros.SameUser;
        }

        if (!followed.HasPublicProfile)
        {
            return FollowerErrros.NonPublicProfile;
        }

        if (await _followerRepository.IsAlreadyFollowingAsync(user.Id, followed.Id, cancellationToken))
        {
            return FollowerErrros.AlreadyFollowing;
        }

        var follower = Follower.Create(user.Id, followed.Id, createdOnUtc);
        _followerRepository.Insert(follower);

        return Result.Success();
    }
}
