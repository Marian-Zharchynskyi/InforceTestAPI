using Domain.Authentications.Users;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> Add(User user, CancellationToken cancellationToken);
    Task<User> Update(User user, CancellationToken cancellationToken);
    Task<User> Delete(User user, CancellationToken cancellationToken);
    Task<User> AddRole(UserId userId, string idRole, CancellationToken cancellationToken);
}