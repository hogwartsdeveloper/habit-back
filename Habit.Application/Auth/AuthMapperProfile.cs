using AutoMapper;
using Habit.Application.Auth.Models;
using Habit.Domain.Entities;

namespace Habit.Application.Auth;

public class AuthMapperProfile : Profile
{
    public AuthMapperProfile()
    {
        CreateMap<RegistrationModel, User>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserVerification, opt => opt.Ignore())
            .ForMember(dst => dst.PasswordHash, opt => opt.Ignore());
    }
}