﻿using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;

namespace Domain.UnitTests.Followers;
public class FollowerServiceTests
{
    private readonly FollowerService _followerService;
    private readonly IFollowerRepository _followerRepositoryMock;
    private static readonly Email Email = Email.Create("fullname@test.com.br");
    private static readonly Name Name = Name.Create("Full Name");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    public FollowerServiceTests()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();
        _followerService = new FollowerService(_followerRepositoryMock);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        //Arrange
        var user = User.Create(Name, Email, hasPublicProfile: false);

        //Act
        var result = await _followerService.StartFollowingAsync(user, user, UtcNow);

        //Assert
        result.Error.Should().Be(FollowerErrros.SameUser);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingNonPublicProfile()
    {
        //Arrange
        var user = User.Create(Name, Email, hasPublicProfile: false);
        var followed = User.Create(Name, Email, hasPublicProfile: false);

        //Act
        var result = await _followerService.StartFollowingAsync(user, followed, UtcNow);

        //Assert
        result.Error.Should().Be(FollowerErrros.NonPublicProfile);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenAlreadyFollowing()
    {
        //Arrange
        var user = User.Create(Name, Email, hasPublicProfile: false);
        var followed = User.Create(Name, Email, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(true);

        //Act
        var result = await _followerService.StartFollowingAsync(user, followed, UtcNow);

        //Assert
        result.Error.Should().Be(FollowerErrros.AlreadyFollowing);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnSuccess_WhenFollowedCreated()
    {
        //Arrange
        var user = User.Create(Name, Email, hasPublicProfile: false);
        var followed = User.Create(Name, Email, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(false);

        //Act
        var result = await _followerService.StartFollowingAsync(user, followed, UtcNow);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task StartFollowingAsync_Should_CallInsertOnRepository_WhenFollowerCreated()
    {
        //Arrange
        var user = User.Create(Name, Email, hasPublicProfile: false);
        var followed = User.Create(Name, Email, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id)
            .Returns(false);

        //Act
        await _followerService.StartFollowingAsync(user, followed, UtcNow);

        //Assert
        _followerRepositoryMock.Received(1)
            .Insert(Arg.Is<Follower>(f => f.UserId == user.Id && f.FollowerId == followed.Id));
    }
}
