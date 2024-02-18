using FluentValidation;
using Habit.Application.Users.Models;

namespace Habit.Application.Users.Validators;

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage("FirstName is required");

        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage("LastName is required");
    }
}