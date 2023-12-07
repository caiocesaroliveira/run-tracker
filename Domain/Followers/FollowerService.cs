using Domain.Users;
using SharedKernel;

namespace Domain.Followers;
public sealed class FollowerService
{
    private readonly IFollowerRepository _followerRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public FollowerService(
        IFollowerRepository followerRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _followerRepository = followerRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> StartFollowingAsync(
        User user,
        User followed,
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

        var follower = Follower.Create(user.Id, followed.Id, _dateTimeProvider.UtcNow);
        _followerRepository.Insert(follower);

        return Result.Success();
    }
}
