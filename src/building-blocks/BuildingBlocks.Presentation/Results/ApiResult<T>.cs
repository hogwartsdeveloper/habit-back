using BuildingBlocks.Errors.Models;

namespace BuildingBlocks.Presentation.Results;

/// <summary>
/// 
/// </summary>
/// <param name="result"></param>
/// <param name="isSuccess"></param>
/// <param name="errors"></param>
/// <typeparam name="T"></typeparam>
public class ApiResult<T>(T? result, bool isSuccess, IEnumerable<ErrorModel>? errors = null)
    : ApiResult(isSuccess, errors)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ApiResult<T> Success(T result)
    {
        return new ApiResult<T>(result, true);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public T? Result { get; private set; } = result;
}