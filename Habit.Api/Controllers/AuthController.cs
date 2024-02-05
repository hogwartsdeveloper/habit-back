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
    public Task<AuthViewModel> SignUp([FromBody] RegistrationModel viewModel)
    {
        return authService.SignUpAsync(viewModel);
    }
    
    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> SignIn([FromBody] LoginModel viewModel)
    {
        return authService.SignInAsync(viewModel);
    }

    [HttpGet("Refresh")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> Refresh()
    {
        var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        return authService.RefreshSession(userEmail!);
    }
}