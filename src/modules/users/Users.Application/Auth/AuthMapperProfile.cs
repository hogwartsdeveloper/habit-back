using AutoMapper;
using Users.Application.Auth.Models;
using Users.Domain.Auth;
using Users.Domain.Users;

namespace Users.Application.Auth;

/// <summary>
/// Профиль маппера аутентификации.
/// </summary>
public class AuthMapperProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса AuthMapperProfile.
    /// </summary>
    public AuthMapperProfile()
    {
        CreateMap<RegistrationModel, User>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserVerification, opt => opt.Ignore())
            .ForMember(dst => dst.PasswordHash, opt => opt.Ignore());

        CreateMap<RefreshToken, RefreshTokenModel>();
    }
}