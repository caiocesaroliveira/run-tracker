using Domain.Users;

namespace Domain.Followers;
public sealed class FollowerService
{
    private readonly IFollowerRepository followerRepository;

    public FollowerService(IFollowerRepository repository) =>
        followerRepository = repository;

    public async void StartFollowing(
        User user,
        User followed,
        DateTime createdOnUtc,
        CancellationToken cancellationToken = default)
    {
        if (user.Id == followed.Id)
        {
            throw new Exception("Can't follow yourself");
        }

        if (!followed.HasPublicProfile)
        {
            throw new Exception("Can't follow non-public profile");
        }

        if (await followerRepository.IsAlreadyFollowingAsync(user.Id, followed.Id, cancellationToken))
        {
            throw new Exception("Already following");
        }

        var follower = Follower.Create(user.Id, followed.Id, createdOnUtc);
        followerRepository.Insert(follower);
    }
}
