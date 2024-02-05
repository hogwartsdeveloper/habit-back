namespace Habit.Application.Mail.Models;

public class MailData
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}