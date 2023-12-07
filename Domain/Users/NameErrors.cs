using SharedKernel;

namespace Domain.Users;

public static class NameErrors
{
    public static readonly Error NullOrEmpty = new("Name.NullOrEmpty", "Name is null or empty");
}