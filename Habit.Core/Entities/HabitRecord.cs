using Habit.Core.Entities.Abstraction;

namespace Habit.Core.Entities;

public class HabitRecord : EntityBase
{
    public required Guid HabitId { get; set; }
    
    public Habit? Habit { get; set; }
    public required DateTime Date { get; set; }
    
    public required bool IsComplete { get; set; }
}