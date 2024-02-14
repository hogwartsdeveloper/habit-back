using FluentValidation;
using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Validators;

public class RequestModelValidator : AbstractValidator<RequestModel>
{
    public RequestModelValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}