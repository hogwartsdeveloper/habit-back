using FluentValidation;
using Habit.Application.Habit.Models;

namespace Habit.Application.Habit.Validators;


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
            .NotEmpty().WithMessage("Title is required");
        
        RuleFor(m => m.StartDate)
            .NotEmpty().WithMessage("StartDate is required");

        RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage("EndDate is required");
    }
}