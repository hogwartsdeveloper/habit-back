using FluentValidation;
using Habit.Application.Auth.Constants;
using Habit.Application.Users.Models;

namespace Habit.Application.Users.Validators;

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
            .NotEmpty().WithMessage(AuthConstants.EmailIsRequired)
            .EmailAddress().WithMessage(AuthConstants.EmailIsInvalid);
    }
}