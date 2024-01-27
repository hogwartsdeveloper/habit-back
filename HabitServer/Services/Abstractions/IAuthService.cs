using HabitServer.Models;

namespace HabitServer.Services.Abstractions;

public interface IAuthService
{
    Task<AuthViewModel> SignUpAsync(RegistrationViewModel viewModel);

    Task<AuthViewModel> SignInAsync(LoginViewModel viewModel);
}