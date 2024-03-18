using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Habit.Application.Errors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Infrastructure.Interceptors;

public class ValidatorInterceptor : IValidatorInterceptor
{
    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        return commonContext;
    }

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

        var error = new Error(statusCode, failure.ErrorMessage, tags);

        return JsonConvert.SerializeObject(error);
    }
}