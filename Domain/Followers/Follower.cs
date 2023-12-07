using SharedKernel;

namespace Domain.Followers;
public sealed class Follower : Entity
{
    private Follower(Guid userId, Guid followerId, DateTime createdOnUtc)
    {
        UserId = userId;
        FollowerId = followerId;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid UserId { get; private set; }
    public Guid FollowerId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static Follower Create(Guid userId, Guid followedId, DateTime createdOnUtc)
    {
        var follower = new Follower(userId, followedId, createdOnUtc);
        follower.Raise(new FollowerCreatedDomainEvent(follower.UserId, follower.FollowerId));
        return follower;
    }
}
