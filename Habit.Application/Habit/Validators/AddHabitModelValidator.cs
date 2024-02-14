using FluentValidation;
using Habit.Application.Habit.Models;

namespace Habit.Application.Habit.Validators;

public class AddHabitModelValidator : AbstractValidator<AddHabitModel>
{
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