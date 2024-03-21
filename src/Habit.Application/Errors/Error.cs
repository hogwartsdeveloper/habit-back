namespace Habit.Application.Errors;

public class Error(
    int statusCode,
    string message,
    Dictionary<string, string>? tags = null)
{
    public int StatusCode { get; private set; } = statusCode;

    public string Message { get; private set; } = message;

    public Dictionary<string, string> Tags { get; private set; } = tags ?? new Dictionary<string, string>();
}