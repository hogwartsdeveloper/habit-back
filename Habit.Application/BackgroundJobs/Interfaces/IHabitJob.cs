namespace Habit.Application.BackgroundJobs.Interfaces;

public interface IHabitJob
{
    public Task CheckIsOverdueAsync();
}