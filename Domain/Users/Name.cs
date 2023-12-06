using Domain.Abstractions;

namespace Domain.Users;

public sealed record Name
{
    private Name(string? value)
    {
        Ensure.NotNullOrEmpty(value);
        Value = value;
    }
    public string Value { get; }

    public static Name Create(string? name)
    {
        return new Name(name);
    }
};
