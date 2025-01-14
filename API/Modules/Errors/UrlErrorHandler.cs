using Application.Urls.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class UrlErrorHandler
{
    public static ObjectResult ToObjectResult(this UrlException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                UrlNotFoundException => StatusCodes.Status404NotFound,
                UrlAlreadyExistsException => StatusCodes.Status409Conflict,
                UrlUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Url error handler does not implemented")
            }
        };
    }
}