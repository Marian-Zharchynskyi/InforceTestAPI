using API.Dtos;
using Application.Common.Interfaces.Queries;
using Domain.Authentications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("roles")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = AuthSettings.AdminRole)]
[ApiController]
public class RoleController(IRoleQueries roleQueries) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var roles = await roleQueries.GetAll(cancellationToken);
        return roles.Select(RoleDto.FromDomainModel).ToList();
    }

    [HttpGet("get-by-id/{roleId:guid}")]
    public async Task<ActionResult<RoleDto>> GetById(
        [FromRoute] Guid roleId,
        CancellationToken cancellationToken)
    {
        var role = await roleQueries.GetById(roleId, cancellationToken);

        return role.Match<ActionResult<RoleDto>>(
            r => RoleDto.FromDomainModel(r),
            () => NotFound());
    }

    [HttpGet("get-by-name/{roleName}")]
    public async Task<ActionResult<RoleDto>> GetByName(
        [FromRoute] string roleName,
        CancellationToken cancellationToken)
    {
        var role = await roleQueries.GetByName(roleName, cancellationToken);

        return role.Match<ActionResult<RoleDto>>(
            r => RoleDto.FromDomainModel(r),
            () => NotFound());
    }
}