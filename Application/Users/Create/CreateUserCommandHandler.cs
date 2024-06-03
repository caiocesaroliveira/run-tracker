using Application.Abstractions.Messaging;
using Domain.Abstractions.Data;
using Domain.Users;
using SharedKernel;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(command.Name);
        if (nameResult.IsFailure)
        {
            return Result.Failure<Guid>(nameResult.Error);
        }

        Result<Email> emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        Email email = emailResult.Value;
        if (!await _userRepository.IsEmailUniqueAsync(email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = User.Create(nameResult.Value, email, command.HasPublicProfile);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return user.Id;
    }
}
