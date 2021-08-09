using System.Collections.Generic;

namespace ITFriends.Infrastructure.Domain.Common
{
    public struct AppUserRole
    {
        public const string Admin = "admin";
        public const string Moderator = "moderator";
        public const string User = "user";

        public static readonly List<string> All = new() {Admin, Moderator, User};
    }
}