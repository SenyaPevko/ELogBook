using Domain.Entities;
using Domain.Entities.Users;
using Domain.Models.Auth;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.Auth;
using Infrastructure.Commands.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Domain.RequestArgs.Auth.RegisterRequest;

namespace ELogBook.Controllers;

[ApiController]
[Route("api/" + "[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse, CreateErrorInfo<InvalidUserReason>>> Register(
        [FromServices] CreateUserCommand command,
        [FromBody] RegisterRequest request)
    {
        var id = Guid.NewGuid();

        return await command.ExecuteAsync(id, request, HttpContext.RequestAborted);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse, UpdateErrorInfo<InvalidUserReason>>> Login(
        [FromServices] LoginUserCommand command,
        [FromBody] LoginRequest request)
    {
        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse, UpdateErrorInfo<InvalidUserReason>>> Refresh(
        [FromServices] RefreshUserTokenCommand command,
        [FromBody] RefreshTokenRequest request)
    {
        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<ActionResult<bool, UpdateErrorInfo<InvalidUserReason>>> Revoke(
        [FromServices] RevokeUserTokenCommand command,
        [FromBody] RevokeTokenRequest request)
    {
        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }
}