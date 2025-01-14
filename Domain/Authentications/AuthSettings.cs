namespace Domain.Authentications;

public static class AuthSettings
{
    public const string UserRole = "Regular";
    public const string AdminRole = "Admin";

    public static readonly List<string> ListOfRoles = new()
    {
        UserRole,
        AdminRole
    };
}