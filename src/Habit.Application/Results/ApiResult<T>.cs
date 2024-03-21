using Habit.Application.Errors;

namespace Habit.Application.Results;

public class ApiResult<T>(T? result, bool isSuccess, IEnumerable<Error>? errors = null)
    : ApiResult(isSuccess, errors)
{
    public static ApiResult<T> Success(T result)
    {
        return new ApiResult<T>(result, true);
    }
    
    public T? Result { get; private set; } = result;
}