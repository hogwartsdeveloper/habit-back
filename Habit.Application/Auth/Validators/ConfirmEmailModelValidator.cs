using FluentValidation;
using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Validators;

/// <summary>
/// Валидатор модели подтверждения email.
/// </summary>
public class ConfirmEmailModelValidator : AbstractValidator<ConfirmEmailModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса ConfirmEmailModelValidator.
    /// </summary>
    public ConfirmEmailModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(m => m.Code)
            .NotEmpty().WithMessage("Code is required")
            .Length(4).WithMessage("The code must be 4 characters long.");
    }
}