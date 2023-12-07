using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_Should_ReturnError_WhenValueIsInvalid(string? value)
    {
        var result = Name.Create(value);
        result.Error.Should().Be(NameErrors.NullOrEmpty);
    }

    [Theory]
    [InlineData("Full Name")]
    public void Constructor_Should_ReturnSuccess_WhenValueIsValid(string value)
    {
        var result = Name.Create(value);
        result.IsSuccess.Should().BeTrue();
    }
}