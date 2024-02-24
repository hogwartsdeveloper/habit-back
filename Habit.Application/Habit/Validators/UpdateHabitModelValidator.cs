using FluentValidation;
using Habit.Application.Habit.Models;

namespace Habit.Application.Habit.Validators;

/// <summary>
/// Валидатор модели для обновления привычки.
/// </summary>
public class UpdateHabitModelValidator : AbstractValidator<UpdateHabitModel>
{
    /// <summary>
    /// Конструктор валидатора модели для обновления привычки.
    /// </summary>
    public UpdateHabitModelValidator()
    {
        RuleFor(m => m.Title)
            .NotEmpty().WithMessage("Title is required");
        
        RuleFor(m => m.StartDate)
            .NotEmpty().WithMessage("StartDate is required");

        RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage("EndDate is required");
    }
}