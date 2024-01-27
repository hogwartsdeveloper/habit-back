using HabitServer.Models;
using HabitServer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HabitServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> SignUp([FromBody] RegistrationViewModel viewModel)
    {
        return authService.SignUpAsync(viewModel);
    }
    
    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> SignIn([FromBody] LoginViewModel viewModel)
    {
        return authService.SignInAsync(viewModel);
    }
}