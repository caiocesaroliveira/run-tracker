using Domain.Abstractions;

namespace Domain.Users;

public sealed record Email
{
    private Email(string? value)
    {
        Ensure.NotNullOrEmpty(value);
        Value = value;
    }
    public string Value { get; }

    public static Email Create(string email)
    {
        return new Email(email);
    }
};