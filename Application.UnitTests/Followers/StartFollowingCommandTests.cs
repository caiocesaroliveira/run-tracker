using Application.Followers.StartFollowing;
using Domain.Abstractions.Data;
using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Application.UnitTests.Followers;
public class StartFollowingCommandTests
{
    private static readonly User User = User.Create(
        Name.Create("Full Name").Value,
        Email.Create("fullname@test.com").Value,
        true);

    private static readonly StartFollowingCommand Command = new(Guid.NewGuid(), Guid.NewGuid());

    private readonly StartFollowingCommandHandler _handler;
    private readonly IFollowerService _followerServiceMock;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StartFollowingCommandTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _followerServiceMock = Substitute.For<IFollowerService>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _handler = new StartFollowingCommandHandler(
            _followerServiceMock,
            _userRepository,
            _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserIsNull()
    {
        //Arrange
        _userRepository.GetByIdAsync(Command.UserId).Returns((User?)null);

        //Act
        Result result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.UserId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenFollowedIsNull()
    {
        //Arrange
        _userRepository.GetByIdAsync(Command.UserId).Returns(User);
        _userRepository.GetByIdAsync(Command.FollowId).Returns((User?)null);

        //Act
        Result result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.FollowId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenStartFollownigFails()
    {
        //Arrange
        _userRepository.GetByIdAsync(Command.UserId).Returns(User);
        _userRepository.GetByIdAsync(Command.FollowId).Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(FollowerErrors.SameUser);

        //Act
        Result result = await _handler.Handle(Command, default);

        //Assert
        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenStartFollownigDoesNotFails()
    {
        //Arrange
        _userRepository.GetByIdAsync(Command.UserId).Returns(User);
        _userRepository.GetByIdAsync(Command.FollowId).Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Result.Success());

        //Act
        Result result = await _handler.Handle(Command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenStartFollownigDoesNotFails()
    {
        //Arrange
        _userRepository.GetByIdAsync(Command.UserId).Returns(User);
        _userRepository.GetByIdAsync(Command.FollowId).Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Result.Success());

        //Act
        await _handler.Handle(Command, default);

        //Assert
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
