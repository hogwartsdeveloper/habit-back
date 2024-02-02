using AutoMapper;
using HabitServer.Entities;
using HabitServer.Models.Habits;

namespace HabitServer.MapperProfiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Habit, HabitViewModel>()
            .ForMember(dst => dst.Records, opt => opt.MapFrom(src => src.HabitRecords));

        CreateMap<AddHabitModel, Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore())
            .ForMember(dst => dst.IsOverdue, opt => opt.Ignore());

        CreateMap<UpdateHabitModel, Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore());

        CreateMap<HabitRecord, HabitRecordViewModel>();

        CreateMap<AddHabitRecordModel, HabitRecord>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.HabitId, opt => opt.Ignore())
            .ForMember(dst => dst.Habit, opt => opt.Ignore());
    }
}