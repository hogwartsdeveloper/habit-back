using AutoMapper;
using HabitServer.Entities;
using HabitServer.Models;

namespace HabitServer.MapperProfiles;

public class RegistrationViewModelProfile : Profile
{
    public RegistrationViewModelProfile()
    {
        CreateMap<RegistrationViewModel, User>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.PasswordHash, opt => opt.Ignore());
    }
}