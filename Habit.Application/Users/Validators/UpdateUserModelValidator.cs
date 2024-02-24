using FluentValidation;
using Habit.Application.Users.Models;

namespace Habit.Application.Users.Validators;

/// <summary>
/// Валидатор для модели обновления информации о пользователе.
/// </summary>
public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UpdateUserModelValidator"/>.
    /// </summary>
    public UpdateUserModelValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage("FirstName is required");

        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage("LastName is required");
    }
}