using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class Habit : EntityBase
{
    private Habit() {}
    public Habit(string title, DateTime startDate, DateTime endDate)
    {
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void SetUser(Guid userId)
    {
        UserId = userId;
    }

    public void SetOverdue(bool isOverdue)
    {
        IsOverdue = isOverdue;
    }
    
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public string Title { get; private set; }
    
    public string? Description { get; private set; }

    public bool IsOverdue { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    public List<HabitRecord>? HabitRecords { get; private set; }
}