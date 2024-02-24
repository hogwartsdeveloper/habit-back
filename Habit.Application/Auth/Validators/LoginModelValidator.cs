using FluentValidation;
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
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(m => m.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters long.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");
    }
}