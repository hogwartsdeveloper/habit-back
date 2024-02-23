using System.Security.Claims;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HabitController(IHabitService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<HabitViewModel>), 200)]
    public Task<List<HabitViewModel>> GetListAsync(CancellationToken cancellationToken)
    {
        return service.GetListAsync(cancellationToken);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(HabitViewModel), 200)]
    public Task<HabitViewModel?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public Task<Guid> AddAsync([FromBody] AddHabitModel viewModel, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        return service.AddAsync(Guid.Parse(userId), viewModel, cancellationToken);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task UpdateAsync(Guid id, [FromBody] UpdateHabitModel viewModel, CancellationToken cancellationToken)
    {
        return service.UpdateAsync(id, viewModel, cancellationToken);
    }

    [HttpPatch("AddRecord/{habitId:guid}")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task AddRecord(Guid habitId, [FromBody] List<HabitRecordViewModel> models,
        CancellationToken cancellationToken)
    {
        return service.AddRecord(habitId, models, cancellationToken);
    }
}