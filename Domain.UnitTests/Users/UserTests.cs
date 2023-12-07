using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Create_Should_ReturnUser_WhenValueIsValid()
    {
        //Arrange
        var email = Email.Create("fullname@test.com.br").Value;
        var name = Name.Create("Full Name").Value;

        //Act
        var user = User.Create(name, email, hasPublicProfile: true);

        //Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomain_WhenValueIsValid()
    {
        //Arrange
        var email = Email.Create("fullname@test.com.br").Value;
        var name = Name.Create("Full Name").Value;

        //Act
        var user = User.Create(name, email, hasPublicProfile: true);

        //Assert
        user.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<UserCreatedDomainEvent>();
    }
}