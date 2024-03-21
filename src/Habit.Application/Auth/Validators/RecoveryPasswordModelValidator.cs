using FluentValidation;
using Habit.Application.Auth.Constants;
using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Validators;

/// <summary>
/// Валидатор модели восстановления пароля.
/// </summary>
public class RecoveryPasswordModelValidator : AbstractValidator<RecoveryPasswordModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса RecoveryPasswordModelValidator.
    /// </summary>
    public RecoveryPasswordModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage(AuthConstants.EmailIsRequired)
            .EmailAddress().WithMessage(AuthConstants.EmailIsInvalid);
        
        RuleFor(m => m.Code)
            .NotEmpty().WithMessage(AuthConstants.CodeIsRequired)
            .Length(4).WithMessage(AuthConstants.CodeLength);
        
        RuleFor(m => m.Password)
            .NotEmpty().WithMessage(AuthConstants.PasswordIsRequired)
            .MinimumLength(5).WithMessage(AuthConstants.PasswordLength)
            .Matches("[0-9]").WithMessage(AuthConstants.PasswordMustOneNumber);

        RuleFor(m => m.ConfirmPassword)
            .NotEmpty().WithMessage(AuthConstants.ConfirmPasswordIsRequired)
            .Equal(m => m.Password).WithMessage(AuthConstants.ConfirmPasswordMatchPassword);
    }
}
