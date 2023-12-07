using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class EmailTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_Should_ReturnError_WhenValueIsNullOrEmpty(string? value)
    {
        var result = Email.Create(value);
        result.Error.Should().Be(EmailErrors.Empty);
    }

    [Theory]
    [InlineData("fullname")]
    public void Constructor_Should_ReturnError_WhenValueIsInvalidFormat(string? value)
    {
        var result = Email.Create(value);
        result.Error.Should().Be(EmailErrors.InvalidFormat);
    }

    [Theory]
    [InlineData("fullname@test.com.br")]
    public void Constructor_Should_ReturnSuccess_WhenValueIsValid(string value)
    {
        var result = Email.Create(value);
        result.IsSuccess.Should().BeTrue();
    }
}