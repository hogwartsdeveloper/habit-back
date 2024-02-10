using System.Security.Claims;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> SignUp([FromBody] RegistrationModel viewModel, CancellationToken cancellationToken)
    {
        return authService.SignUpAsync(viewModel, cancellationToken);
    }
    
    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> SignIn([FromBody] LoginModel viewModel, CancellationToken cancellationToken)
    {
        return authService.SignInAsync(viewModel, cancellationToken);
    }

    [HttpGet("Refresh")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> Refresh(CancellationToken cancellationToken)
    {
        var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        return authService.RefreshSessionAsync(userEmail!, cancellationToken);
    }

    [HttpPost("ConfirmEmail")]
    [ProducesResponseType(200)]
    public Task ConfirmEmailAsync([FromBody] ConfirmEmailModel model, CancellationToken cancellationToken)
    {
        return authService.ConfirmEmailAsync(model, cancellationToken);
    }
}