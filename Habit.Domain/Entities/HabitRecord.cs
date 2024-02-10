using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class HabitRecord : EntityBase
{
    public required Guid HabitId { get; set; }
    
    public Domain.Entities.Habit? Habit { get; set; }
    public required DateTime Date { get; set; }
    
    public required bool IsComplete { get; set; }
}