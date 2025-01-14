using Domain.Urls;

namespace Application.Urls.Exceptions;

public abstract class UrlException : Exception
{
    public UrlId UrlId { get; }

    protected UrlException(UrlId id, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        UrlId = id;
    }
}

public class UrlNotFoundException : UrlException
{
    public UrlNotFoundException(UrlId id)
        : base(id, $"URL with id: {id} not found") { }
}

public class UrlAlreadyExistsException : UrlException
{
    public UrlAlreadyExistsException(UrlId id)
        : base(id, $"URL already exists: {id}") { }
}

public class UrlUnknownException : UrlException
{
    public UrlUnknownException(UrlId id, Exception innerException)
        : base(id, $"Unknown exception for URL with id: {id}", innerException) { }
}
