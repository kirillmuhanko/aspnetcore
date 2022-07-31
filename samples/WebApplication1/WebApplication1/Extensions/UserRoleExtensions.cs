using WebApplication1.Models.Enums;

namespace WebApplication1.Extensions;

public static class UserRoleExtensions
{
    public static UserRoles Add(this UserRoles userRoles, UserRoles role)
    {
        return userRoles | role;
    }

    public static UserRoles Remove(this UserRoles userRoles, UserRoles role)
    {
        return userRoles & ~role;
    }

    public static bool IsUserInRole(this UserRoles userRoles, UserRoles role)
    {
        return userRoles.HasFlag(role);
    }
}