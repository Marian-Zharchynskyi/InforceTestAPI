namespace Application.Urls.Commands;

using Common;
using Exceptions;
using Common.Interfaces.Repositories;
using Domain.Urls;
using Domain.Authentications.Users;
using MediatR;

public record CreateUrlCommand : IRequest<Result<Url, UrlException>>
{
    public required string OriginalUrl { get; init; }
    public required Guid CreatedBy { get; init; }
}

public class CreateUrlCommandHandler(
    IUrlRepository urlRepository)
    : IRequestHandler<CreateUrlCommand, Result<Url, UrlException>>
{
    public async Task<Result<Url, UrlException>> Handle(
        CreateUrlCommand request,
        CancellationToken cancellationToken)
    {
        var existingUrl =
            await urlRepository.GetByUrl(request.OriginalUrl, cancellationToken);

        var createdById = new UserId(request.CreatedBy);

        return await existingUrl.Match(
            c => Task.FromResult<Result<Url, UrlException>>(
                new UrlAlreadyExistsException(c.Id)),
            async () => await CreateUrlEntity(request.OriginalUrl, createdById, cancellationToken));
    }

    private async Task<Result<Url, UrlException>> CreateUrlEntity(
        string originalUrl,
        UserId createdBy,
        CancellationToken cancellationToken)
    {
        try
        {
            var url = Url.New(originalUrl, createdBy);

            return await urlRepository.Add(url, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UrlUnknownException(UrlId.Empty(), exception);
        }
    }
}