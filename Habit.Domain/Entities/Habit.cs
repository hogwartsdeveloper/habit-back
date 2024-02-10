using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class Habit : EntityBase
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }

    public bool IsOverdue { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    
    public List<HabitRecord>? HabitRecords { get; set; }
}