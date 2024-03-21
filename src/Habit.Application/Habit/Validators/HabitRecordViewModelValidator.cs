using FluentValidation;
using Habit.Application.Habit.Constants;
using Habit.Application.Habit.Models;

namespace Habit.Application.Habit.Validators;


/// <summary>
/// Валидатор модели представления записи привычки.
/// </summary>
public class HabitRecordViewModelValidator : AbstractValidator<HabitRecordViewModel>
{
    /// <summary>
    /// Конструктор валидатора модели представления записи привычки.
    /// </summary>
    public HabitRecordViewModelValidator()
    {
        RuleFor(m => m.Date)
            .NotEmpty().WithMessage(HabitConstant.RecordDateIsRequired);
    }
}