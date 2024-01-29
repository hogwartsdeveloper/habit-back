namespace HabitServer.Models.Habits;

public class UpdateHabitModel
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }

    public bool IsOverdue { get; set; }
    
    public required DateTime StartDate { get; set; }
    
    public required DateTime EndDate { get; set; }
}