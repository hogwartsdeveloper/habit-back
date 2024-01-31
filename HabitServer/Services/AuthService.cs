using System.Net;
using AutoMapper;
using HabitServer.Entities;
using HabitServer.Exception;
using HabitServer.Models;
using HabitServer.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HabitServer.Services;

public class AuthService(
    ApplicationDbContext dbContext,
    IMapper mapper,
    ISecurityService securityService) : IAuthService
{
    public async Task<AuthViewModel> SignUpAsync(RegistrationViewModel viewModel)
    {
        await ValidateUserNotExists(viewModel.Email);
        
        var entity = mapper.Map<User>(viewModel);
        entity.PasswordHash = securityService.HashPassword(viewModel.Password);
        
        var addedEntity = await dbContext.Users.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        return new AuthViewModel { Token = securityService.GenerateToken(addedEntity.Entity) };
    }

    public async Task<AuthViewModel> SignInAsync(LoginViewModel viewModel)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Email == viewModel.Email);
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Email or password wrong");
        }
        
        var isPassValid = securityService.VerifyPassword(viewModel.Password, user.PasswordHash);
        if (!isPassValid)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Email or password wrong");
        }
        
        return new AuthViewModel { Token = securityService.GenerateToken(user) };
    }

    private async Task ValidateUserNotExists(string email)
    {
        if (await dbContext.Users.AnyAsync(user => user.Email == email))
        {
            throw new HttpException(HttpStatusCode.BadRequest, "User already exists");
        }
    }
}