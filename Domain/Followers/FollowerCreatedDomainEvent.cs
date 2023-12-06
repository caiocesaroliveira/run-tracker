using Domain.Abstractions;

namespace Domain.Followers;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid Followerid) : IDomainEvent;