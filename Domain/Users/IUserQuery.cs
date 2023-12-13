namespace Domain.Users;
public interface IUserQuery
{
    IQueryable<User> QueryUsers { get; }
}
