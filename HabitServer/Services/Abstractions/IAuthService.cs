using HabitServer.Models;

namespace HabitServer.Services.Abstractions;

public interface IAuthService
{
    Task<AuthViewModel> RegistrationAsync(RegistrationViewModel viewModel);
}