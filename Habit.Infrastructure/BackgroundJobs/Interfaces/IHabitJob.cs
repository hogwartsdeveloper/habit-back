namespace Infrastructure.BackgroundJobs.Interfaces;

public interface IHabitJob
{
    public Task CheckIsOverdueAsync();
}