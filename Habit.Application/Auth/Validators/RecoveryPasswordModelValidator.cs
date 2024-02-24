using FluentValidation;
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
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");
        
        RuleFor(m => m.Code)
            .NotEmpty().WithMessage("Code is required")
            .Length(4).WithMessage("The code must be 4 characters long.");
        
        RuleFor(m => m.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.");

        RuleFor(m => m.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(m => m.Password).WithMessage("The password confirmation must match the password.");
    }
}
