using AutoMapper;
using HabitServer.Entities;
using HabitServer.Models;
using HabitServer.Models.Habits;

namespace HabitServer.MapperProfiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Habit, HabitViewModel>();

        CreateMap<AddHabitModel, Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore())
            .ForMember(dst => dst.IsOverdue, opt => opt.Ignore());

        CreateMap<UpdateHabitModel, Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore());

        CreateMap<AddHabitCalendarModel, HabitRecord>();
    }
}