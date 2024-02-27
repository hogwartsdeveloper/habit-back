using Habit.Api.Controllers.Abstractions;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

/// <summary>
/// Контроллер для управления привычками.
/// </summary>
[Authorize]
public class HabitController(IHabitService service) : BaseController
{
    /// <summary>
    /// Получает список привычек.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список моделей привычек.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<HabitViewModel>), 200)]
    public Task<List<HabitViewModel>> GetListAsync(CancellationToken cancellationToken)
    {
        return service.GetListAsync(cancellationToken);
    }
    
    /// <summary>
    /// Получает привычку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель привычки.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(HabitViewModel), 200)]
    public Task<HabitViewModel?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }
    
    /// <summary>
    /// Добавляет новую привычку.
    /// </summary>
    /// <param name="viewModel">Модель для добавления привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор добавленной привычки.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public Task<Guid> AddAsync([FromBody] AddHabitModel viewModel, CancellationToken cancellationToken)
    {
        return service.AddAsync(GetCurrentUserId()!.Value, viewModel, cancellationToken);
    }
    
    /// <summary>
    /// Обновляет существующую привычку.
    /// </summary>
    /// <param name="id">Идентификатор привычки.</param>
    /// <param name="viewModel">Модель для обновления привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task UpdateAsync(Guid id, [FromBody] UpdateHabitModel viewModel, CancellationToken cancellationToken)
    {
        return service.UpdateAsync(id, viewModel, cancellationToken);
    }

    /// <summary>
    /// Добавляет запись для указанной привычки.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки.</param>
    /// <param name="models">Список моделей записей привычек.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPatch("AddRecord/{habitId:guid}")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task AddRecord(Guid habitId, [FromBody] List<HabitRecordViewModel> models,
        CancellationToken cancellationToken)
    {
        return service.AddRecord(habitId, models, cancellationToken);
    }
}