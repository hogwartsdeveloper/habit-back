using FluentValidation;
using Habits.Application.Constants;
using Habits.Application.Models;

namespace Habits.Application.Validators;

/// <summary>
/// Валидатор модели для добавления привычки.
/// </summary>
public class AddHabitModelValidator : AbstractValidator<AddHabitModel>
{
    /// <summary>
    /// Конструктор валидатора модели для добавления привычки.
    /// </summary>
    public AddHabitModelValidator()
    {
        RuleFor(m => m.Title)
            .NotEmpty().WithMessage(HabitConstant.TitleIsRequired);
        
        RuleFor(m => m.StartDate)
            .NotEmpty().WithMessage(HabitConstant.StartDateIsRequired);

        RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage(HabitConstant.EndDateIsRequired);
    }
}