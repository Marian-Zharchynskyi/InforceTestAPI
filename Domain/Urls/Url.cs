using Domain.Users;

namespace Domain.Urls;

public class Url
{
    public UrlId Id { get; }
    public string OriginalUrl { get; private set; }
    public string ShortenedUrl { get; private set; }
    public UserId CreatedBy { get; }
    public DateTime CreatedDate { get; }

    private Url(UrlId id, string originalUrl, string shortenedUrl, UserId createdBy, DateTime createdDate)
    {
        Id = id;
        OriginalUrl = originalUrl;
        ShortenedUrl = shortenedUrl;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
    }

    public static Url New(string originalUrl, UserId createdBy)
    {
        string shortenedUrl = GenerateShortenedUrl(originalUrl);
        return new Url(UrlId.New(), originalUrl, shortenedUrl, createdBy, DateTime.UtcNow);
    }

    public void Update(string originalUrl)
    {
        OriginalUrl = originalUrl;
        ShortenedUrl = GenerateShortenedUrl(originalUrl);
    }
    
    public static string GenerateShortenedUrl(string originalUrl)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(originalUrl));
        return Convert.ToBase64String(hashBytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=')
            .Substring(0, 8); 
    }
}
