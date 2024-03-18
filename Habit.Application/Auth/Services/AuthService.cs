using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habit.Application.Auth.Constants;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Habit.Application.BrokerMessage;
using Habit.Application.Errors;
using Habit.Application.Mail.Models;
using Habit.Application.Repositories;
using Habit.Domain.Entities;
using Habit.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Auth.Services;

/// <summary>
/// Сервис аутентификации.
/// </summary>
public class AuthService(
    IRepository<User> userRepository,
    IRepository<UserVerify> userVerifyRepository,
    IRepository<RefreshToken> refreshTokenRepository,
    IMapper mapper,
    ISecurityService securityService,
    IBrokerMessageService brokerMessageService) : IAuthService
{
    /// <inheritdoc />
    public async Task<AuthViewModel> SignUpAsync(RegistrationModel model, CancellationToken cancellationToken)
    {
        await ValidateUserNotExists(model.Email);
        
        var entity = mapper.Map<User>(model);
        
        entity.Registration(securityService.HashPassword(model.Password));

        var addedUserId = await userRepository.AddAsync(entity, cancellationToken);
        entity.Id = addedUserId;
        
        var token = securityService.GenerateToken(entity);
        var refreshToken = securityService.GenerateRefreshToken();
        
        await refreshTokenRepository
            .AddAsync(
                new RefreshToken(entity.Id, refreshToken.Token, refreshToken.Expires),
                cancellationToken);

        await SendVerifyMessage(entity, UserVerifyType.Email, AuthConstants.ConfirmEmail);

        return new AuthViewModel { AccessToken = token };
    }

    /// <inheritdoc />
    public async Task<AuthViewModel> SignInAsync(LoginModel model, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, AuthConstants.EmailOrPasswordWrong);
        }
        
        var isPassValid = securityService.VerifyPassword(model.Password, user.PasswordHash);
        if (!isPassValid)
        {
            throw new HttpException(HttpStatusCode.BadRequest, AuthConstants.EmailOrPasswordWrong);
        }
        
        var token = securityService.GenerateToken(user);
        var refreshToken = securityService.GenerateRefreshToken();
        
        var refreshTokenData = await refreshTokenRepository
            .GetListAsync(e => e.UserId == user.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (refreshTokenData != null)
        {
            refreshTokenData.UpdateToken(refreshToken.Token, refreshToken.Expires);
            await refreshTokenRepository.UpdateAsync(refreshTokenData, cancellationToken);
        }
        else
        {
            await refreshTokenRepository
                .AddAsync(
                    new RefreshToken(user.Id, refreshToken.Token, refreshToken.Expires),
                    cancellationToken);
        }

        return new AuthViewModel { AccessToken = token };
    }

    /// <inheritdoc />
    public async Task<AuthViewModel> RefreshSessionAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, AuthConstants.UserNotFound);
        }

        var refreshToken = await refreshTokenRepository
            .GetListAsync(e => e.UserId == user.Id)
            .ProjectTo<RefreshTokenModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (refreshToken is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, AuthConstants.TokenInvalid);
        }

        var tokenIsValid = securityService.VerifyRefreshToken(refreshToken);

        if (!tokenIsValid)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, AuthConstants.TokenInvalid);
        }


        await userRepository.UpdateAsync(user, cancellationToken);
        
        var token = securityService.GenerateToken(user);

        return new AuthViewModel { AccessToken = token };
    }

    /// <inheritdoc />
    public async Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(model.Email, cancellationToken);
        await ValidateUserVerifyAsync(user.Id, model.Code, UserVerifyType.Email, cancellationToken);
        
        user.ConfirmEmail();
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RequestForChangeAsync(
        string email,
        UserVerifyType verifyType,
        CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(email, cancellationToken);
        
        string? messageSubject = null;
        switch (verifyType)
        {
            case UserVerifyType.Email:
                messageSubject = AuthConstants.ConfirmEmail;
                break;
            case UserVerifyType.PasswordRecovery:
                messageSubject = AuthConstants.RequestForChangePassword;
                break;
        }

        if (messageSubject is not null)
        {
            await SendVerifyMessage(user, verifyType, messageSubject);
        }
        
    }
    
    /// <inheritdoc />
    public async Task RecoveryPasswordAsync(RecoveryPasswordModel model, CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(model.Email, cancellationToken);
        await ValidateUserVerifyAsync(user.Id, model.Code, UserVerifyType.PasswordRecovery, cancellationToken);
        
        user.ChangePasswordHash(securityService.HashPassword(model.Password));
        await userRepository.UpdateAsync(user, cancellationToken);
        
    }

    
    private async Task ValidateUserNotExists(string email)
    {
        if (await userRepository.AnyAsync(user => user.Email == email))
        {
            throw new HttpException(HttpStatusCode.BadRequest, AuthConstants.UserAlreadyExists);
        }
    }

    private async Task<User> GetAndValidateUserExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, AuthConstants.UserNotFound);
        }

        return user;
    }

    private async Task ValidateUserVerifyAsync(
        Guid userId,
        string code,
        UserVerifyType verifyType,
        CancellationToken cancellationToken)
    {
        var isVerify = await userVerifyRepository
            .AnyAsync(e =>
                e.UserId == userId && e.Code == code && e.VerifyType == verifyType && e.Exp >= DateTime.UtcNow,
                cancellationToken);

        if (!isVerify)
        {
            throw new HttpException(HttpStatusCode.BadRequest, AuthConstants.CodeIsNotCorrect);
        }
    }

    private async Task SendVerifyMessage(User user, UserVerifyType userVerifyType, string subject)
    {
        var code = new Random().Next(1000, 9999).ToString();

        await userVerifyRepository.AddAsync(new UserVerify(
            user.Id,
            code,
            DateTime.UtcNow.AddMinutes(15),
            userVerifyType));
        
        brokerMessageService.SendMessage(
            new MailData
            {
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Subject = subject,
                Body = code
            });
    }
}