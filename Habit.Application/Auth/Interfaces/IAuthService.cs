using Habit.Application.Auth.Models;
using Habit.Domain.Enums;

namespace Habit.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthViewModel> SignUpAsync(RegistrationModel model, CancellationToken cancellationToken);

    Task<AuthViewModel> SignInAsync(LoginModel model, CancellationToken cancellationToken);
    
    Task<AuthViewModel> RefreshSessionAsync(string email, CancellationToken cancellationToken);

    Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken);

    Task RequestForChangeAsync(string email, UserVerifyType verifyType, CancellationToken cancellationToken);
}