using Domain.Urls;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IUrlRepository
{
    Task<Url> Add(Url url, CancellationToken cancellationToken);
    Task<Url> Update(Url url, CancellationToken cancellationToken);
    Task<Url> Delete(Url url, CancellationToken cancellationToken);
    Task<Option<Url>> GetByUrl(string url, CancellationToken cancellationToken);
}