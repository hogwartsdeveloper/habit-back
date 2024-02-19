using System.Security.Claims;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("image")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task AddImage([FromForm] AddUserImageModel model, CancellationToken cancellationToken)
    {
        var userIdData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdData, out var userId);

        return service.AddImageAsync(userId, model.File, cancellationToken);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task UpdateAsync([FromBody] UpdateUserModel model, CancellationToken cancellationToken)
    {
        var userIdData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdData, out var userId);
        
        return service.UpdateAsync(userId, model, cancellationToken);
    }
}