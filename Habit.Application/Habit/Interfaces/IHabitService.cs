using Habit.Application.Habit.Models;

namespace Habit.Application.Habit.Interfaces;

public interface IHabitService
{
    Task<Guid> AddAsync(Guid userId, AddHabitModel model, CancellationToken cancellationToken = default);

    Task<List<HabitViewModel>> GetListAsync(CancellationToken cancellationToken = default);

    Task<HabitViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Guid id, UpdateHabitModel model, CancellationToken cancellationToken = default);

    Task AddRecord(Guid habitId, List<HabitRecordViewModel> models, CancellationToken cancellationToken = default);
}