using Habit.Application.Errors;

namespace Habit.Application.Results;

public class ApiResult<T>
{
    private ApiResult(T? result, bool isSuccess, IEnumerable<Error>? errors = null)
    {
        Result = result;
        IsSuccess = isSuccess;
        Errors = errors ?? Array.Empty<Error>();
    }

    public static ApiResult<T> Success(T result)
    {
        return new ApiResult<T>(result, true);
    }

    public static ApiResult<T> Failure(IEnumerable<Error> errors)
    {
        return new ApiResult<T>(default, false, errors);
    } 
    public T? Result { get; private set; }

    public bool IsSuccess { get; private set; }

    public IEnumerable<Error> Errors { get; private set; }
}