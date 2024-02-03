namespace Habit.Application.Habit.Models;

public class AddHabitModel
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}