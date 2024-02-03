namespace Habit.Application.Habit.Models;

public class HabitViewModel
{
    public Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }

    public bool IsOverdue { get; set; }
    
    public required DateTime StartDate { get; set; }
    
    public required DateTime EndDate { get; set; }
    
    public List<HabitRecordViewModel>? Records { get; set; }
}