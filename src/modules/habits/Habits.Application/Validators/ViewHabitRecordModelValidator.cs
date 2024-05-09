using FluentValidation;
using Habits.Application.Constants;
using Habits.Application.Models;

namespace Habits.Application.Validators;

/// <summary>
/// Валидатор модели представления записи привычки.
/// </summary>
public class ViewHabitRecordModelValidator : AbstractValidator<ViewHabitRecordModel>
{
    /// <summary>
    /// Конструктор валидатора модели представления записи привычки.
    /// </summary>
    public ViewHabitRecordModelValidator()
    {
        RuleFor(m => m.Date)
            .NotEmpty().WithMessage(HabitConstant.RecordDateIsRequired);
    }
}