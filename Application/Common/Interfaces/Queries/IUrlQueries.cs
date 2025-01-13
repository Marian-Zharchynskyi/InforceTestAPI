using Domain.Urls;

namespace Application.Common.Interfaces.Queries;

public interface IUrlQueries
{
    Task<IReadOnlyList<Url>> GetAll(CancellationToken cancellationToken);
    Task<Url?> GetById(UrlId id, CancellationToken cancellationToken);
    Task<Url?> GetByShortenedUrl(string shortenedUrl, CancellationToken cancellationToken);
}