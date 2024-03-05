using FluentValidation;
using Habit.Application.Habit.Constants;
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
            .NotEmpty().WithMessage(HabitConstant.TitleIsRequired);
        
        RuleFor(m => m.StartDate)
            .NotEmpty().WithMessage(HabitConstant.StartDateIsRequired);

        RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage(HabitConstant.EndDateIsRequired);
    }
}