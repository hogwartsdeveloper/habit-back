using FluentValidation;
using Habit.Application.Auth.Constants;
using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Validators;

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
            .NotEmpty().WithMessage(AuthConstants.EmailIsRequired)
            .EmailAddress().WithMessage(AuthConstants.EmailIsInvalid);

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage(AuthConstants.PasswordIsRequired)
            .MinimumLength(5).WithMessage(AuthConstants.PasswordLength)
            .Matches("[0-9]").WithMessage(AuthConstants.PasswordMustOneNumber);

        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage(AuthConstants.FirstNameIsRequired);
        
        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage(AuthConstants.LastNameIsRequired);
    }
}
