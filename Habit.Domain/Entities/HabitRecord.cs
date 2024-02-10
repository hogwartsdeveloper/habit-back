using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class HabitRecord : EntityBase
{
    private HabitRecord() {}
    public HabitRecord(Guid habitId, DateTime date)
    {
        HabitId = habitId;
        Date = date;
    }
    public void SetHabit(Guid habitId)
    {
        HabitId = habitId;
    }
    public Guid HabitId { get; private set; }
    public Habit? Habit { get; private set; }
    public DateTime Date { get; private set; }
    
    public bool IsComplete { get; private set; }
}