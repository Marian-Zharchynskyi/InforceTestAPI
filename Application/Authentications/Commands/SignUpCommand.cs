using Application.Authentications.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Services;
using Application.Services.HashPasswordService;
using Application.Services.TokenService;
using Application.ViewModels;
using Domain.Authentications.Users;
using MediatR;
using Domain.Authentications;
using Domain.Authentications.Roles;
using FluentValidation;
using Optional;

namespace Application.Authentications.Commands;

public class SignUpCommand : IRequest<Result<JwtVM, AuthenticationException>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string? Name { get; init; }
}

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUserQueries userQueries,
    IRoleQueries roleQueries,
    IJwtTokenService jwtTokenService,
    IHashPasswordService hashPasswordService)
    : IRequestHandler<SignUpCommand, Result<JwtVM, AuthenticationException>>
{
    public async Task<Result<JwtVM, AuthenticationException>> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userQueries.GetByEmail(request.Email, cancellationToken);

        return await existingUser.Match(
            u => Task.FromResult<Result<JwtVM, AuthenticationException>>(
                new UserByThisEmailAlreadyExistsException(u.Id)),
            async () => await SignUp(request.Email, request.Password, request.Name, cancellationToken));
    }

    private async Task<Result<JwtVM, AuthenticationException>> SignUp(
        string email,
        string password,
        string? name,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.New();
            var entity = User.New(userId, email, name, hashPasswordService.HashPassword(password));

            await userRepository.Add(entity, cancellationToken);

            var role = await roleQueries.GetByName(AuthSettings.UserRole, cancellationToken);

            var token = await jwtTokenService
                .GenerateTokensAsync(await userRepository
                    .AddRole(entity.Id, role.ToString(), cancellationToken), cancellationToken);

            return token;
        }
        catch (Exception exception)
        {
            return new AuthenticationUnknownException(UserId.Empty(), exception);
        }
    }
}