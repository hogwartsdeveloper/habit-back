using BuildingBlocks.Errors.Models;
using BuildingBlocks.Presentation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BuildingBlocks.Validation.Attributes;

/// <summary>
/// Атрибут фильтра действий, проверяющий корректность модели.
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Выполняет проверку валидности модели перед выполнением действия контроллера.
    /// </summary>
    /// <param name="context">Контекст выполнения действия,
    /// содержащий информацию о состоянии модели и другие параметры.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => JsonConvert.DeserializeObject<ErrorModel>(e.ErrorMessage))
            .ToArray();

        context.Result = new BadRequestObjectResult(ApiResult.Failure(errors));
    }
}