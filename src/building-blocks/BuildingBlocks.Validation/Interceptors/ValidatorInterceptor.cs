using System.Net;
using BuildingBlocks.Errors.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BuildingBlocks.Validation.Interceptors;

/// <summary>
/// Интерцептор валидатора,
/// который позволяет изменять контекст валидации и результаты валидации перед и после выполнения валидации в ASP.NET.
/// </summary>
public class ValidatorInterceptor : IValidatorInterceptor
{
    /// <summary>
    /// Метод вызывается перед выполнением валидации в ASP.NET. Позволяет модифицировать контекст валидации.
    /// </summary>
    /// <param name="actionContext">Контекст действия, содержащий информацию о текущем запросе и контроллере.</param>
    /// <param name="commonContext">Общий контекст валидации, который может быть изменен перед валидацией.</param>
    /// <returns>Модифицированный или исходный контекст валидации.</returns>
    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        return commonContext;
    }

    /// <summary>
    /// Метод вызывается после выполнения валидации в ASP.NET. Позволяет изменять результаты валидации.
    /// </summary>
    /// <param name="actionContext">Контекст действия, содержащий информацию о текущем запросе и контроллере.</param>
    /// <param name="validationContext">Контекст валидации, содержащий информацию о процессе валидации.</param>
    /// <param name="result">Результат валидации,
    /// который может быть изменен в зависимости от логики обработки ошибок.</param>
    /// <returns>Модифицированный результат валидации, содержащий возможные ошибки.</returns>
    public ValidationResult AfterAspNetValidation(
        ActionContext actionContext,
        IValidationContext validationContext,
        ValidationResult result)
    {
        var failures = result.Errors
            .Select(error => new ValidationFailure(error.PropertyName, SerializeError(error)));

        return new ValidationResult(failures);
    }

    private static string SerializeError(ValidationFailure failure)
    {
        var tags = new Dictionary<string, string>();
        tags.Add(nameof(failure.PropertyName), failure.PropertyName);

        var isParse = int.TryParse(failure.ErrorCode, out var statusCode);

        if (!isParse)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
        }

        var error = new ErrorModel(statusCode, failure.ErrorMessage, tags);

        return JsonConvert.SerializeObject(error);
    }
}