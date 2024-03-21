using FluentValidation;
using Habit.Application.Auth.Constants;
using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Validators;

/// <summary>
/// Валидатор модели входа пользователя.
/// </summary>
public class LoginModelValidator : AbstractValidator<LoginModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса LoginModelValidator.
    /// </summary>
    public LoginModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage(AuthConstants.EmailIsRequired)
            .EmailAddress().WithMessage(AuthConstants.EmailIsInvalid);

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage(AuthConstants.PasswordIsRequired)
            .MinimumLength(5).WithMessage(AuthConstants.PasswordLength)
            .Matches(@"[0-9]").WithMessage(AuthConstants.PasswordMustOneNumber);
    }
}