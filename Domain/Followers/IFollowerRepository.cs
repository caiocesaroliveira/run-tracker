namespace Domain.Followers;

public interface IFollowerRepository
{
    Task<bool> IsAlreadyFollowingAsync(Guid userId, Guid follwedId, CancellationToken cancellationToken);
    void Insert(Follower follower);
}