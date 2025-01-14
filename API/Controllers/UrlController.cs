namespace API.Controllers;

using Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Urls.Commands;
using Domain.Urls;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("urls")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]

public class UrlsController(
    ISender sender,
    IUrlQueries urlQueries) 
    : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<UrlDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await urlQueries.GetAll(cancellationToken);

        return entities.Select(UrlDto.FromDomainModel).ToList();
    }

    [Authorize]
    [HttpGet("get-by-id/{urlId:guid}")]
    public async Task<ActionResult<UrlDto>> Get([FromRoute] Guid urlId, CancellationToken cancellationToken)
    {
        var entity = await urlQueries.GetById(new UrlId(urlId), cancellationToken);

        return entity.Match<ActionResult<UrlDto>>(
            u => UrlDto.FromDomainModel(u),
            () => NotFound());
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<UrlDto>> Create(
        [FromBody] UrlDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateUrlCommand
        {
            OriginalUrl = request.OriginalUrl,
            CreatedBy = request.CreatedBy
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UrlDto>>(
            u => UrlDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }

    [Authorize]
    [HttpDelete("delete/{urlId:guid}")]
    public async Task<ActionResult<UrlDto>> Delete([FromRoute] Guid urlId, CancellationToken cancellationToken)
    {
        var input = new DeleteUrlCommand
        {
            UrlId = urlId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UrlDto>>(
            u => UrlDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }
}
