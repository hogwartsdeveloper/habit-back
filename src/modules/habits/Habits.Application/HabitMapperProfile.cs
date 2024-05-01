using AutoMapper;
using Habits.Application.Models;
using Habits.Domain.Habits;

namespace Habits.Application;

/// <summary>
/// Профиль сопоставления для привычек.
/// </summary>
public class HabitMapperProfile : Profile
{
    public HabitMapperProfile()
    {
        CreateMap<Domain.Habits.Habit, ViewHabitModel>()
            .ForMember(dst => dst.Records, opt => opt.MapFrom(src => src.HabitRecords))
            .ForMember(dst => dst.CountComplete, opt => opt.MapFrom(src => src.HabitRecords != null
                ? src.HabitRecords.Count(r => r.IsComplete)
                : 0));
        
        CreateMap<AddHabitModel, Domain.Habits.Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.IsOverdue, opt => opt.Ignore());

        CreateMap<UpdateHabitModel, Domain.Habits.Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore());

        CreateMap<HabitRecord, ViewHabitRecordModel>();

        CreateMap<ViewHabitRecordModel, HabitRecord>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.HabitId, opt => opt.Ignore())
            .ForMember(dst => dst.Habit, opt => opt.Ignore());
    }
}