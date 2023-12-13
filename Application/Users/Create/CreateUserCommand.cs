using Application.Abstractions.Messaging;

namespace Application.Users.Create;

public sealed record CreateUserCommand(string Name, string Email, bool HasPublicProfile) : ICommand<Guid>;
