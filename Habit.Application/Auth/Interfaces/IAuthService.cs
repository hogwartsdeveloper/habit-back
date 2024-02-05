using Habit.Application.Auth.Models;

namespace Habit.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthViewModel> SignUpAsync(RegistrationModel model);

    Task<AuthViewModel> SignInAsync(LoginModel model);
    
    Task<AuthViewModel> RefreshSession(string email);
}