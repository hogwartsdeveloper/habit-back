using BuildingBlocks.Errors.Models;

namespace BuildingBlocks.Presentation.Results;

/// <summary>
/// Представляет результат вызова API.
/// </summary>
/// <param name="isSuccess">Флаг успеха операции.</param>
/// <param name="errors">Коллекция ошибок, если они есть.</param>
public class ApiResult(bool isSuccess, IEnumerable<ErrorModel>? errors = null)
{
    /// <summary>
    /// Возвращает значение, указывающее, был ли вызов API успешным.
    /// </summary>
    public bool IsSuccess { get; private set; } = isSuccess;

    /// <summary>
    /// Возвращает список ошибок, связанных с вызовом API.
    /// </summary>
    public IEnumerable<ErrorModel> Errors { get; private set; } = errors ?? Array.Empty<ErrorModel>();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static ApiResult Success()
    {
        return new ApiResult(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static ApiResult Failure(IEnumerable<ErrorModel>? errors = null)
    {
        return new ApiResult(false, errors);
    } 
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ApiResult Failure(ErrorModel error)
    {
        return new ApiResult(false, [error]);
    } 
}