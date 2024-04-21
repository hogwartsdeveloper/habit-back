using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingBlocks.Entity.Interfaces;
using BuildingBlocks.Errors.Exceptions;
using Microsoft.EntityFrameworkCore;
using Users.Application.Auth.Interfaces;
using Users.Application.Auth.Models;
using Users.Application.Constants;
using Users.Domain.Auth;
using Users.Domain.Auth.Enums;
using Users.Domain.Users;

namespace Users.Application.Auth.Services;

/// <summary>
/// Сервис аутентификации.
/// </summary>
public class AuthService(
    IRepository<User> userRepo,
    IRepository<UserVerify> userVerifyRepo,
    IRepository<RefreshToken> refreshTokenRepo,
    ISecurityService securityService,
    IMapper mapper) : IAuthService
{
    /// <inheritdoc />
    public async Task<ViewAuthModel> SignUpAsync(RegistrationModel model, CancellationToken cancellationToken)
    {
        await ValidateUserNotExists(model.Email);
        
        var entity = mapper.Map<User>(model);
        
        entity.Registration(securityService.HashPassword(model.Password));

        var addedUserId = await userRepo.AddAsync(entity, cancellationToken);
        entity.Id = addedUserId;
        
        var token = securityService.GenerateToken(entity);
        var refreshToken = securityService.GenerateRefreshToken();
        
        await refreshTokenRepo
            .AddAsync(
                new RefreshToken(entity.Id, refreshToken.Token, refreshToken.Expires),
                cancellationToken);

        // IntegrationEvent
        
        return new ViewAuthModel { AccessToken = token };
    }
    
    /// <inheritdoc />
    public async Task<ViewAuthModel> SignInAsync(LoginModel model, CancellationToken cancellationToken)
    {
        var user = await userRepo
            .GetListAsync(e => e.Email == model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, UserConstants.EmailOrPasswordWrong);
        }
        
        var isPassValid = securityService.VerifyPassword(model.Password, user.PasswordHash);
        if (!isPassValid)
        {
            throw new HttpException(HttpStatusCode.BadRequest, UserConstants.EmailOrPasswordWrong);
        }
        
        var token = securityService.GenerateToken(user);
        var refreshToken = securityService.GenerateRefreshToken();
        
        var refreshTokenData = await refreshTokenRepo
            .GetListAsync(e => e.UserId == user.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (refreshTokenData != null)
        {
            refreshTokenData.UpdateToken(refreshToken.Token, refreshToken.Expires);
            await refreshTokenRepo.UpdateAsync(refreshTokenData, cancellationToken);
        }
        else
        {
            await refreshTokenRepo
                .AddAsync(
                    new RefreshToken(user.Id, refreshToken.Token, refreshToken.Expires),
                    cancellationToken);
        }

        return new ViewAuthModel { AccessToken = token };
    }

    /// <inheritdoc />
    public async Task<ViewAuthModel> RefreshSessionAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepo
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, UserConstants.UserNotFound);
        }

        var refreshToken = await refreshTokenRepo
            .GetListAsync(e => e.UserId == user.Id)
            .ProjectTo<RefreshTokenModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (refreshToken is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, UserConstants.TokenInvalid);
        }

        var tokenIsValid = securityService.VerifyRefreshToken(refreshToken);

        if (!tokenIsValid)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, UserConstants.TokenInvalid);
        }


        await userRepo.UpdateAsync(user, cancellationToken);
        
        var token = securityService.GenerateToken(user);

        return new ViewAuthModel { AccessToken = token };
    }

    /// <inheritdoc />
    public async Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(model.Email, cancellationToken);
        await ValidateUserVerifyAsync(user.Id, model.Code, UserVerifyType.Email, cancellationToken);
        
        user.ConfirmEmail();
        await userRepo.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RequestForChangeAsync(string email, UserVerifyType verifyType, CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(email, cancellationToken);
        
        string? messageSubject = null;
        switch (verifyType)
        {
            case UserVerifyType.Email:
                messageSubject = UserConstants.ConfirmEmail;
                break;
            case UserVerifyType.PasswordRecovery:
                messageSubject = UserConstants.RequestForChangePassword;
                break;
        }

        if (messageSubject is not null)
        {
            // IntegrationEvent
        }
    }

    /// <inheritdoc />
    public async Task RecoveryPasswordAsync(RecoveryPasswordModel model, CancellationToken cancellationToken)
    {
        var user = await GetAndValidateUserExistsAsync(model.Email, cancellationToken);
        await ValidateUserVerifyAsync(user.Id, model.Code, UserVerifyType.PasswordRecovery, cancellationToken);
        
        user.ChangePasswordHash(securityService.HashPassword(model.Password));
        await userRepo.UpdateAsync(user, cancellationToken);
    }
    
    private async Task ValidateUserVerifyAsync(
        Guid userId,
        string code,
        UserVerifyType verifyType,
        CancellationToken cancellationToken)
    {
        var isVerify = await userVerifyRepo
            .AnyAsync(e =>
                    e.UserId == userId && e.Code == code && e.VerifyType == verifyType && e.Exp >= DateTime.UtcNow,
                cancellationToken);

        if (!isVerify)
        {
            throw new HttpException(HttpStatusCode.BadRequest, UserConstants.CodeIsNotCorrect);
        }
    }
    
    private async Task ValidateUserNotExists(string email)
    {
        if (await userRepo.AnyAsync(user => user.Email == email))
        {
            var tags = new Dictionary<string, string>{{ nameof(email), email }};
            
            throw new HttpException(
                HttpStatusCode.BadRequest,
                UserConstants.UserAlreadyExists,
                tags);
        }
    }
    
    private async Task<User> GetAndValidateUserExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepo
            .GetListAsync(e => e.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new HttpException(HttpStatusCode.Unauthorized, UserConstants.UserNotFound);
        }

        return user;
    }
}