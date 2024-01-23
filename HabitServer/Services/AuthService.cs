using HabitServer.Entities;
using HabitServer.Models;
using HabitServer.Services.Abstractions;

namespace HabitServer.Services;

public class AuthService(ApplicationDbContext dbContext, ISecurityService securityService) : IAuthService
{
    public Task<AuthViewModel> RegistrationAsync(LoginViewModel viewModel)
    {
        var passwordHash = securityService.HashPassword(viewModel.Password);

        return Task.FromResult(new AuthViewModel { Token = securityService.GenerateToken() });
    }
}