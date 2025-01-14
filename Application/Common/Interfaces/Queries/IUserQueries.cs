using Domain.Authentications.Users;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IUserQueries
{
    Task<IReadOnlyList<User>> GetAll(CancellationToken cancellationToken);
    Task<Option<User>> GetById(UserId id, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}