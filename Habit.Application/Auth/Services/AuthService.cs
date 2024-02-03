using System.Net;
using AutoMapper;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Habit.Core.Entities;
using Habit.Core.Exceptions;
using Habit.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Auth.Services;

public class AuthService(
    IRepository<User> userRepository,
    IMapper mapper,
    ISecurityService securityService) : IAuthService
{
    public async Task<AuthViewModel> SignUpAsync(RegistrationModel model)
    {
        await ValidateUserNotExists(model.Email);
        
        var entity = mapper.Map<User>(model);
        entity.PasswordHash = securityService.HashPassword(model.Password);

        var addedUserId = await userRepository.AddAsync(entity);
        entity.Id = addedUserId;
        
        return new AuthViewModel { AccessToken = securityService.GenerateToken(entity) };
    }

    public async Task<AuthViewModel> SignInAsync(LoginModel model)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == model.Email)
            .FirstOrDefaultAsync();
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Email or password wrong");
        }
        
        var isPassValid = securityService.VerifyPassword(model.Password, user.PasswordHash);
        if (!isPassValid)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Email or password wrong");
        }
        
        return new AuthViewModel { AccessToken = securityService.GenerateToken(user) };
    }
    
    private async Task ValidateUserNotExists(string email)
    {
        if (await userRepository.AnyAsync(user => user.Email == email))
        {
            throw new HttpException(HttpStatusCode.BadRequest, "User already exists");
        }
    }
}