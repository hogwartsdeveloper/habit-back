using Habit.Application.Errors;

namespace Habit.Application.Results;

public class ApiResult(bool isSuccess, IEnumerable<Error>? errors = null)
{
    public bool IsSuccess { get; private set; } = isSuccess;

    public IEnumerable<Error> Errors { get; private set; } = errors ?? Array.Empty<Error>();
    
    public static ApiResult Success()
    {
        return new ApiResult(true);
    }

    public static ApiResult Failure(IEnumerable<Error> errors)
    {
        return new ApiResult(false, errors);
    } 
}