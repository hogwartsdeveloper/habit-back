using AutoMapper;
using HabitServer.Entities;
using HabitServer.Models;
using HabitServer.Services.Abstractions;

namespace HabitServer.Services;

public class AuthService(ApplicationDbContext dbContext, IMapper mapper, ISecurityService securityService) : IAuthService
{
    public async Task<AuthViewModel> RegistrationAsync(RegistrationViewModel viewModel)
    {
        var entity = mapper.Map<User>(viewModel);
        entity.PasswordHash = securityService.HashPassword(viewModel.Password);
        await dbContext.Users.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        return new AuthViewModel { Token = securityService.GenerateToken() };
    }
}