using HabitServer.Models;
using HabitServer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HabitServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(AuthViewModel), 200)]
    public Task<AuthViewModel> Registration([FromBody] RegistrationViewModel viewModel)
    {
        return authService.RegistrationAsync(viewModel);
    }
}