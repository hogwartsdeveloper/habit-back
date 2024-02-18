using AutoMapper;
using Habit.Application.Users.Models;
using Habit.Domain.Entities;

namespace Habit.Application.Users;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UpdateUserModel, User>()
            .ForMember(dst => dst.Email, opt => opt.Ignore())
            .ForMember(dst => dst.IsEmailConfirmed, opt => opt.Ignore())
            .ForMember(dst => dst.PasswordHash, opt => opt.Ignore())
            .ForMember(dst => dst.BirthDay, opt => opt.Ignore())
            .ForMember(dst => dst.Habits, opt => opt.Ignore())
            .ForMember(dst => dst.UserVerification, opt => opt.Ignore())
            .ForMember(dst => dst.RefreshToken, opt => opt.Ignore());
    }
}