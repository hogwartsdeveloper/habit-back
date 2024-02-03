namespace Habit.Application.Habit.Models;

public class HabitRecordViewModel
{
    public required DateTime Date { get; set; }
    public bool IsComplete { get; set; }
}