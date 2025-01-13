using Domain.Urls;

namespace Application.Common.Interfaces.Repositories;

public interface IUrlRepository
{
    Task<Url> Add(Url url, CancellationToken cancellationToken);
    Task<Url> Update(Url url, CancellationToken cancellationToken);
    Task<Url> Delete(Url url, CancellationToken cancellationToken);
}