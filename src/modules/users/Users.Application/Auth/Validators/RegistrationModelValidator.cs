using FluentValidation;
using Users.Application.Auth.Models;
using Users.Application.Constants;

namespace Users.Application.Auth.Validators;

/// <summary>
/// Валидатор модели регистрации пользователя.
/// </summary>
public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса RegistrationModelValidator.
    /// </summary>
    public RegistrationModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage(UserConstants.EmailIsRequired)
            .EmailAddress().WithMessage(UserConstants.EmailIsInvalid);

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage(UserConstants.PasswordIsRequired)
            .MinimumLength(5).WithMessage(UserConstants.PasswordLength)
            .Matches("[0-9]").WithMessage(UserConstants.PasswordMustOneNumber);

        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage(UserConstants.FirstNameIsRequired);
        
        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage(UserConstants.LastNameIsRequired);
    }
}
