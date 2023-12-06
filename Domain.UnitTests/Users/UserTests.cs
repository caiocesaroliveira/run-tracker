using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Create_Should_ReturnUser_WhenValueIsValid()
    {
        //Arrange
        var email = new Email("fullname@teste.com.br");
        var name = new Name("Full Name");
        var hasPublicProfile = true;

        //Act
        var user = User.Create(name, email, hasPublicProfile);

        //Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomain_WhenValueIsValid()
    {
        //Arrange
        var email = new Email("fullname@teste.com.br");
        var name = new Name("Full Name");
        var hasPublicProfile = true;

        //Act
        var user = User.Create(name, email, hasPublicProfile);

        //Assert
        user.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<UserCreatedDomainEvent>();
    }
}