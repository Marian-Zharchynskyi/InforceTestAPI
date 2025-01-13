using Domain.Roles;

namespace Domain.Users;

public class User
{
    public UserId Id { get; }
    public string Email { get; private set; }
    public string? Name { get; private set; }
    public string PasswordHash { get; }
    public List<Role> Roles { get; private set; } = new List<Role>();

    private User(UserId id, string email, string? name, string passwordHash)
    {
        Id = id;
        Email = email;
        Name = name;
        PasswordHash = passwordHash;
    }

    public static User New(UserId id, string email, string? name, string passwordHash)
        => new(id, email, name, passwordHash);

    public void UpdateUser(string email, string? name)
    {
        Email = email;
        Name = name;
    }

    public void SetRoles(List<Role> roles)
        => Roles = roles;
}