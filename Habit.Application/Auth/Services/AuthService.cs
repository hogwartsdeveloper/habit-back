using System.Net;
using AutoMapper;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Habit.Application.BrokerMessage;
using Habit.Application.Mail.Models;
using Habit.Application.Repositories;
using Habit.Core.Enums;
using Habit.Core.Exceptions;
using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Auth.Services;

public class AuthService(
    IRepository<User> userRepository,
    IRepository<UserVerify> userVerifyRepository,
    IMapper mapper,
    ISecurityService securityService,
    IBrokerMessageService brokerMessageService) : IAuthService
{
    public async Task<AuthViewModel> SignUpAsync(RegistrationModel model, CancellationToken cancellationToken)
    {
        await ValidateUserNotExists(model.Email);
        
        var entity = mapper.Map<User>(model);
        var tokens = securityService.GenerateToken(entity);
        
        entity.Registration(securityService.HashPassword(model.Password), tokens.RefreshToken);

        var addedUserId = await userRepository.AddAsync(entity, cancellationToken);
        entity.Id = addedUserId;

        await SendVerifyEmailAsync(entity);
        
        return new AuthViewModel { AccessToken = tokens.AccessToken };
    }

    public async Task<AuthViewModel> SignInAsync(LoginModel model, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        
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
        user.UpdateRefreshToken(tokens.RefreshToken);

        await userRepository.UpdateAsync(user, cancellationToken);
        
        return new AuthViewModel { AccessToken = tokens.AccessToken };
    }

    public async Task<AuthViewModel> RefreshSessionAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
        
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
        
        user.UpdateRefreshToken(tokens.RefreshToken);
        await userRepository.UpdateAsync(user, cancellationToken);

        return new AuthViewModel { AccessToken = tokens.AccessToken };
    }

    public async Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(model.Email, cancellationToken);
        
        var userVerify = await userVerifyRepository
            .GetListAsync(e => e.UserId == user.Id && e.Code == model.Code && e.Exp >= DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);

        if (userVerify is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Code is not correct");
        }

        user.ConfirmEmail();
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    private async Task ValidateUserNotExists(string email)
    {
        if (await userRepository.AnyAsync(user => user.Email == email))
        {
            throw new HttpException(HttpStatusCode.BadRequest, "User already exists");
        }
    }

    private async Task<User> GetAndValidateUserExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, "User not found!");
        }

        return user;
    }

    private async Task SendVerifyEmailAsync(User user)
    {
        var code = new Random().Next(1000, 9999).ToString();
        
        await userVerifyRepository.AddAsync(new UserVerify(
            user.Id,
            code,
            DateTime.UtcNow.AddMinutes(15),
            UserVerifyType.Email)
        );
        
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