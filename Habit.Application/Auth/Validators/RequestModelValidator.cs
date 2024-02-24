using FluentValidation;
using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Validators;

/// <summary>
/// Валидатор модели запроса.
/// </summary>
public class RequestModelValidator : AbstractValidator<RequestModel>
{
    
    /// <summary>
    /// Инициализирует новый экземпляр класса RequestModelValidator.
    /// </summary>
    public RequestModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}