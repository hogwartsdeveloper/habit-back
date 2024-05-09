using FluentValidation;
using Users.Application.Auth.Models;
using Users.Application.Constants;

namespace Users.Application.Auth.Validators;

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
            .NotEmpty().WithMessage(UserConstants.EmailIsRequired)
            .EmailAddress().WithMessage(UserConstants.EmailIsInvalid);
        
        RuleFor(m => m.Code)
            .NotEmpty().WithMessage(UserConstants.CodeIsRequired)
            .Length(4).WithMessage(UserConstants.CodeLength);
        
        RuleFor(m => m.Password)
            .NotEmpty().WithMessage(UserConstants.PasswordIsRequired)
            .MinimumLength(5).WithMessage(UserConstants.PasswordLength)
            .Matches("[0-9]").WithMessage(UserConstants.PasswordMustOneNumber);

        RuleFor(m => m.ConfirmPassword)
            .NotEmpty().WithMessage(UserConstants.ConfirmPasswordIsRequired)
            .Equal(m => m.Password).WithMessage(UserConstants.ConfirmPasswordMatchPassword);
    }
}
