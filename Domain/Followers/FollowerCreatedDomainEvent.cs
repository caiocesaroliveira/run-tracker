using SharedKernel;

namespace Domain.Followers;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid Followerid) : IDomainEvent;