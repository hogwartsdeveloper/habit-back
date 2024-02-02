using HabitServer.Models.Base;

namespace HabitServer.Models.Habits;

public class HabitViewModel : EntityViewModel
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }

    public bool IsOverdue { get; set; }
    
    public required DateTime StartDate { get; set; }
    
    public required DateTime EndDate { get; set; }
    
    public List<HabitRecordViewModel>? Records { get; set; }
}