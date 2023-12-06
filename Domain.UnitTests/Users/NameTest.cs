using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class NameTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_Should_ThrowArgumentException_WhenValueIsInvalid(string? value)
    {
        Name Action() => Name.Create(value);

        FluentActions.Invoking(Action).Should().ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should().Be("value");
    }
}