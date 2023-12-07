using SharedKernel;

namespace Domain.Users;

public sealed record Name
{
    private Name(string value) => Value = value;
    public string Value { get; }

    public static Result<Name> Create(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<Name>(NameErrors.NullOrEmpty);
        }
        return new Name(name);
    }
};
