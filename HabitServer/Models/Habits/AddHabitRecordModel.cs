namespace HabitServer.Models.Habits;

public class AddHabitRecordModel
{
    public required DateTime Date { get; set; }
    
    public required bool isComplate { get; set; }
}