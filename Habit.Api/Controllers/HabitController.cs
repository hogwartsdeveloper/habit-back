using Habit.Api.Controllers.Abstractions;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Models;
using Habit.Application.Results;
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
    [ProducesResponseType(typeof(ApiResult<List<HabitViewModel>>), 200)]
    public async Task<ApiResult<List<HabitViewModel>>> GetListAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetListAsync(GetCurrentUserId()!.Value, cancellationToken);
        
        return ApiResult<List<HabitViewModel>>.Success(result);
    }

    /// <summary>
    /// Получает группу привычек асинхронно.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая группу привычек.</returns>
    [HttpGet("Group")]
    [ProducesResponseType(typeof(ApiResult<HabitGroupViewsModel>), 200)]
    public async Task<ApiResult<HabitGroupViewsModel>> GetListGroupAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetListGroupAsync(GetCurrentUserId()!.Value, cancellationToken);
        
        return ApiResult<HabitGroupViewsModel>.Success(result);
    }
    
    /// <summary>
    /// Получает привычку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель привычки.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResult<HabitViewModel?>), 200)]
    public async Task<ApiResult<HabitViewModel?>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        
        return ApiResult<HabitViewModel?>.Success(result);
    }
    
    /// <summary>
    /// Добавляет новую привычку.
    /// </summary>
    /// <param name="viewModel">Модель для добавления привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор добавленной привычки.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<Guid>), 200)]
    public async Task<ApiResult<Guid>> AddAsync([FromBody] AddHabitModel viewModel, CancellationToken cancellationToken)
    {
        var result = await service.AddAsync(GetCurrentUserId()!.Value, viewModel, cancellationToken);

        return ApiResult<Guid>.Success(result);
    }
    
    /// <summary>
    /// Обновляет существующую привычку.
    /// </summary>
    /// <param name="id">Идентификатор привычки.</param>
    /// <param name="viewModel">Модель для обновления привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> UpdateAsync(Guid id, [FromBody] UpdateHabitModel viewModel, CancellationToken cancellationToken)
    {
        await service.UpdateAsync(id, viewModel, cancellationToken);
        
        return ApiResult.Success();
    }

    /// <summary>
    /// Добавляет запись для указанной привычки.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки.</param>
    /// <param name="models">Список моделей записей привычек.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPatch("AddRecord/{habitId:guid}")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> AddRecord(Guid habitId, [FromBody] List<HabitRecordViewModel> models,
        CancellationToken cancellationToken)
    {
        await service.AddRecord(habitId, models, cancellationToken);
        
        return ApiResult.Success();
    }

    /// <summary>
    /// Удаляет привычку с указанным идентификатором.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая операцию удаления.</returns>
    [HttpDelete("{habitId:guid}")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> Delete(Guid habitId, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(habitId, cancellationToken);
        
        return ApiResult.Success();
    }
}