using FluentValidation;
using Users.Application.Auth.Models;
using Users.Application.Constants;

namespace Users.Application.Auth.Validators;

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
            .NotEmpty().WithMessage(UserConstants.EmailIsRequired)
            .EmailAddress().WithMessage(UserConstants.EmailIsInvalid);

        RuleFor(m => m.Code)
            .NotEmpty().WithMessage(UserConstants.CodeIsRequired)
            .Length(4).WithMessage(UserConstants.CodeLength);
    }
}