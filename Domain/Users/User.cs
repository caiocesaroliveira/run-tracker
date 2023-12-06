﻿using Domain.Abstractions;

namespace Domain.Users;

public class User : Entity
{
    private User(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    public Name Name { get; private set; }

    public static User Create(Name name)
    {
        var user = new User(Guid.NewGuid(), name);
        user.Raise(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}
