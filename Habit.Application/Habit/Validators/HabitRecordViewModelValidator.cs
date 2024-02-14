using FluentValidation;
using Habit.Application.Habit.Models;

namespace Habit.Application.Habit.Validators;

public class HabitRecordViewModelValidator : AbstractValidator<HabitRecordViewModel>
{
    public HabitRecordViewModelValidator()
    {
        RuleFor(m => m.Date)
            .NotEmpty().WithMessage("Date is required");
    }
}