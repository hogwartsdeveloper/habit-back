using FluentValidation;
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
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");

        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage("FirstName is required.");
        
        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage("LastName is required.");
    }
}
