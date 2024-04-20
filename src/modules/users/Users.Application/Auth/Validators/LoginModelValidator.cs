using FluentValidation;
using Users.Application.Auth.Models;
using Users.Application.Constants;

namespace Users.Application.Auth.Validators;

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
            .NotEmpty().WithMessage(UserConstants.EmailIsRequired)
            .EmailAddress().WithMessage(UserConstants.EmailIsInvalid);

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage(UserConstants.PasswordIsRequired)
            .MinimumLength(5).WithMessage(UserConstants.PasswordLength)
            .Matches(@"[0-9]").WithMessage(UserConstants.PasswordMustOneNumber);
    }
}