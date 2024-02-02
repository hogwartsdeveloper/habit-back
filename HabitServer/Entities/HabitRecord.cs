using HabitServer.Entities.Abstractions;

namespace HabitServer.Entities;

public class HabitRecord : Entity
{
    public required Guid HabitId { get; set; }
    
    public Habit? Habit { get; set; }
    public required DateTime Date { get; set; }
    
    public required bool IsComplete { get; set; }
}