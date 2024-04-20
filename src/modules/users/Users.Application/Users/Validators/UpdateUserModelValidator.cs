using FluentValidation;
using Users.Application.Constants;
using Users.Application.Users.Models;

namespace Users.Application.Users.Validators;

/// <summary>
/// Валидатор для модели обновления информации о пользователе.
/// </summary>
public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UpdateUserModelValidator"/>.
    /// </summary>
    public UpdateUserModelValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage(UserConstants.FirstNameIsRequired);

        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage(UserConstants.LastNameIsRequired);
    }
}