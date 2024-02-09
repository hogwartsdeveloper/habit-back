using System.Net;
using AutoMapper;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Habit.Application.BrokerMessage;
using Habit.Application.Mail.Models;
using Habit.Application.Repositories;
using Habit.Core.Entities;
using Habit.Core.Enums;
using Habit.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Auth.Services;

public class AuthService(
    IRepository<User> userRepository,
    IRepository<UserVerify> userVerifyRepository,
    IMapper mapper,
    ISecurityService securityService,
    IBrokerMessageService brokerMessageService) : IAuthService
{
    public async Task<AuthViewModel> SignUpAsync(RegistrationModel model)
    {
        await ValidateUserNotExists(model.Email);
        
        var entity = mapper.Map<User>(model);
        var tokens = securityService.GenerateToken(entity);

        entity.RefreshToken = tokens.RefreshToken;
        entity.PasswordHash = securityService.HashPassword(model.Password);

        var addedUserId = await userRepository.AddAsync(entity);
        entity.Id = addedUserId;

        await SendVerifyEmailAsync(entity);
        
        return new AuthViewModel { AccessToken = tokens.AccessToken };
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

        var tokens = securityService.GenerateToken(user);
        user.RefreshToken = tokens.RefreshToken;

        await userRepository.UpdateAsync(user);
        
        return new AuthViewModel { AccessToken = tokens.AccessToken };
    }

    public async Task<AuthViewModel> RefreshSession(string email)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync();
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, "User not found!");
        }

        var tokenIsValid = await securityService.ValidateTokenAsync(user.RefreshToken!);

        if (!tokenIsValid)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, "Token invalid");
        }

        var tokens = securityService.GenerateToken(user);
        
        user.RefreshToken = tokens.RefreshToken;
        await userRepository.UpdateAsync(user);

        return new AuthViewModel { AccessToken = tokens.AccessToken };
    }

    private async Task ValidateUserNotExists(string email)
    {
        if (await userRepository.AnyAsync(user => user.Email == email))
        {
            throw new HttpException(HttpStatusCode.BadRequest, "User already exists");
        }
    }

    private async Task SendVerifyEmailAsync(User user)
    {
        var code = new Random().Next(1000, 9999).ToString();

        await userVerifyRepository.AddAsync(new UserVerify
        {
            UserId = user.Id,
            Code = code,
            Exp = DateTime.UtcNow.AddMinutes(15),
            VerifyType = UserVerifyType.Email
        });
        
        brokerMessageService.SendMessage(
            new MailData
            {
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Subject = "Confirm email",
                Body = code
            });
    }
}