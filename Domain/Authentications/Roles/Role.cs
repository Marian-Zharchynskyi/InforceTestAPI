using Domain.Authentications.Users;

namespace Domain.Authentications.Roles;

public class Role
{
    public Guid Id { get; }
    public string Name { get; }
    public List<User> Users { get; } = new();

    public Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Role New(string name)
        => new(Guid.NewGuid(), name);
}