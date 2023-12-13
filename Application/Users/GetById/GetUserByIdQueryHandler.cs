using Application.Abstractions.Messaging;
using Domain.Users;
using SharedKernel;

namespace Application.Users.GetById;

internal sealed record GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserQuery _userQuery;

    public GetUserByIdQueryHandler(IUserQuery userQuery)
    {
        _userQuery = userQuery;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = _userQuery.QueryUsers
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name.Value,
                Email = u.Email.Value,
                HasPublicProfile = u.HasPublicProfile,
            })
            .FirstOrDefault();

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
        }

        return await Task.FromResult(user);
    }
}
