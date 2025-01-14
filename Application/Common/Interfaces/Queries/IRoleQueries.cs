using Domain.Authentications.Roles;

namespace Application.Common.Interfaces.Queries;

public interface IRoleQueries
{
    Task<IReadOnlyList<Role>> GetAll(CancellationToken cancellationToken);
    Task<Role?> GetById(Guid id, CancellationToken cancellationToken);
    Task<Role?> GetByName(string name, CancellationToken cancellationToken);
}