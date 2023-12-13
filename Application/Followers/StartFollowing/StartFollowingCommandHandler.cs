
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Followers;
using Domain.Users;
using SharedKernel;

namespace Application.Followers.StartFollowing;

internal sealed class StartFollowingCommandHandler : ICommandHandler<StartFollowingCommand>
{
    private readonly IFollowerService _followerService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StartFollowingCommandHandler(
        IFollowerService followerService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _followerService = followerService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(StartFollowingCommand command, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            return UserErrors.NotFound(command.UserId);
        }

        User? followed = await _userRepository.GetByIdAsync(command.FollowId, cancellationToken);
        if (followed is null)
        {
            return UserErrors.NotFound(command.FollowId);
        }

        Result result = await _followerService
            .StartFollowingAsync(user, followed, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
