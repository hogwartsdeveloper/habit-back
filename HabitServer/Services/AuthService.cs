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
    public async Task<AuthViewModel> RegistrationAsync(RegistrationViewModel viewModel)
    {
        await ValidateRegistration(viewModel.Email);
        var entity = mapper.Map<User>(viewModel);
        entity.PasswordHash = securityService.HashPassword(viewModel.Password);
        
        await dbContext.Users.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        return new AuthViewModel { Token = securityService.GenerateToken() };
    }

    private async Task ValidateRegistration(string email)
    {
        if (await dbContext.Users.AnyAsync(user => user.Email == email))
        {
            throw new HttpException(HttpStatusCode.BadRequest, "User already exists");
        }
    }
}