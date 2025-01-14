using Domain.Urls;

namespace API.Dtos;

public record UrlDto(Guid? Id, string OriginalUrl, string ShortenedUrl, Guid CreatedBy, DateTime CreatedDate)
{
    public static UrlDto FromDomainModel(Url url)
        => new(
            url.Id.Value, 
            url.OriginalUrl, 
            url.ShortenedUrl, 
            url.CreatedBy.Value, 
            url.CreatedDate);
}