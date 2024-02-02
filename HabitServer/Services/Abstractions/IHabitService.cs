using HabitServer.Models.Habits;

namespace HabitServer.Services.Abstractions;

public interface IHabitService
{
    Task<Guid> AddAsync(Guid userId, AddHabitModel viewModel, CancellationToken cancellationToken = default);

    Task<List<HabitViewModel>> GetListAsync(CancellationToken cancellationToken = default);

    Task<HabitViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Guid id, UpdateHabitModel viewModel, CancellationToken cancellationToken = default);

    Task AddRecord(Guid habitId, List<AddHabitRecordModel> models, CancellationToken cancellationToken = default);
}