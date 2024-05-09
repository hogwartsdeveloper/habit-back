using FluentValidation;
using Users.Application.Constants;
using Users.Application.Users.Models;

namespace Users.Application.Users.Validators;

/// <summary>
/// Валидатор модели для обновления адреса электронной почты.
/// </summary>
public class UpdateEmailModelValidator : AbstractValidator<UpdateEmailModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр валидатора модели для обновления адреса электронной почты.
    /// </summary>
    public UpdateEmailModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage(UserConstants.EmailIsRequired)
            .EmailAddress().WithMessage(UserConstants.EmailIsInvalid);
    }
}