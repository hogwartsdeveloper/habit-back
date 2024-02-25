using FluentValidation;
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
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}