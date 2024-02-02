using HabitServer.Entities.Abstractions;

namespace HabitServer.Entities;

public class HabitRecord : Entity
{
    public required Guid HabitId { get; set; }
    
    public Habit? Habit { get; set; }
    public DateTime Date { get; set; }
    
    public bool IsComplete { get; set; }
}