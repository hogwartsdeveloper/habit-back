using Habit.Application.Errors;
using Habit.Application.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Infrastructure.Attributes;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => JsonConvert.DeserializeObject<Error>(e.ErrorMessage))
            .ToArray();

        context.Result = new BadRequestObjectResult(ApiResult.Failure(errors));
    }
}