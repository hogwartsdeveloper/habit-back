using FluentValidation;
using Habit.Application.Auth.Constants;
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
            .NotEmpty().WithMessage(AuthConstants.EmailIsRequired)
            .EmailAddress().WithMessage(AuthConstants.EmailIsInvalid);

        RuleFor(m => m.Code)
            .NotEmpty().WithMessage(AuthConstants.CodeIsRequired)
            .Length(4).WithMessage(AuthConstants.CodeLength);
    }
}