namespace Domain.Urls;

public record UrlId(Guid Value)
{
    public static UrlId New() => new(Guid.NewGuid());
    public static UrlId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}