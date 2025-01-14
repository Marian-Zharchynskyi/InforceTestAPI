using Domain.Urls;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IUrlQueries
{
    Task<IReadOnlyList<Url>> GetAll(CancellationToken cancellationToken);
    Task<Option<Url>> GetById(UrlId id, CancellationToken cancellationToken);
}