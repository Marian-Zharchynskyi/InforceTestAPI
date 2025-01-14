namespace Application.Urls.Commands;

using Application.Common.Interfaces.Queries;
using Common;
using Exceptions;
using Common.Interfaces.Repositories;
using Domain.Urls;
using MediatR;

public record DeleteUrlCommand : IRequest<Result<Url, UrlException>>
{
    public required Guid UrlId { get; init; }
}

public class DeleteUrlCommandHandler(
    IUrlRepository urlRepository,
    IUrlQueries urlQueries)
    : IRequestHandler<DeleteUrlCommand, Result<Url, UrlException>>
{
    public async Task<Result<Url, UrlException>> Handle(
        DeleteUrlCommand request,
        CancellationToken cancellationToken)
    {
        var urlId = new UrlId(request.UrlId);

        var url = await urlQueries.GetById(urlId, cancellationToken);

        return await url.Match(
            async u => await DeleteUrlEntity(u, cancellationToken),
            () => Task.FromResult<Result<Url, UrlException>>(
                new UrlNotFoundException(urlId)));
    }

    private async Task<Result<Url, UrlException>> DeleteUrlEntity(
        Url url,
        CancellationToken cancellationToken)
    {
        try
        {
            return await urlRepository.Delete(url, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UrlUnknownException(url.Id, exception);
        }
    }
}