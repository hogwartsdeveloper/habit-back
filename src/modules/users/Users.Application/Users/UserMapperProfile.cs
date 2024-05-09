using AutoMapper;
using Users.Application.Users.Models;
using Users.Domain.Users;

namespace Users.Application.Users;

/// <summary>
/// Профиль маппинга для модели пользователя.
/// </summary>
public class UserMapperProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserMapperProfile"/>.
    /// </summary>
    public UserMapperProfile()
    {
        CreateMap<UpdateUserModel, User>()
            .ForMember(dst => dst.Email, opt => opt.Ignore())
            .ForMember(dst => dst.IsEmailConfirmed, opt => opt.Ignore())
            .ForMember(dst => dst.PasswordHash, opt => opt.Ignore())
            .ForMember(dst => dst.BirthDay, opt => opt.Ignore())
            .ForMember(dst => dst.UserVerification, opt => opt.Ignore())
            .ForMember(dst => dst.RefreshToken, opt => opt.Ignore());

        CreateMap<User, ViewUserModel>();
    }
}