using Domain.Abstractions;

namespace Domain.Users;

public sealed record Email
{
    public Email(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        Value = value;
    }
    public string Value { get; }
};