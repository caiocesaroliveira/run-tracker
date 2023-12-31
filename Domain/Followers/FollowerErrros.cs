﻿using Domain.Abstractions;

namespace Domain.Followers;

public static class FollowerErrros
{
    public static readonly Error SameUser = new("Followers.SameUser", "Can't follow yourself");
    public static readonly Error NonPublicProfile = new("Followers.NonPublicProfile", "Can't follow non-public profile");
    public static readonly Error AlreadyFollowing = new("Followers.AlreadyFollowing", "Already following");
}