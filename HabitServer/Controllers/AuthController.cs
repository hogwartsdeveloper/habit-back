using HabitServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabitServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(SecurityTokenService tokenService) : ControllerBase
{
    [HttpPost]
    public IActionResult Registration()
    {
        return Ok(tokenService.GenerateToken());
    }
}