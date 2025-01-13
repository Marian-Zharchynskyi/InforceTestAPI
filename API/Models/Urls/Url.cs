namespace API.Models.Urls;

public class Url
{
    public Guid Id { get; set; } 
    public string OriginalUrl { get; set; } 
    public string ShortenedUrl { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ExpirationDate { get; set; } 
}