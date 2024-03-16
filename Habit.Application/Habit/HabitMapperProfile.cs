using AutoMapper;
using Habit.Application.Habit.Models;
using Habit.Domain.Entities;

namespace Habit.Application.Habit;

/// <summary>
/// Профиль сопоставления для привычек.
/// </summary>
public class HabitMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля сопоставления для привычек.
    /// </summary>
    public HabitMapperProfile()
    {
        CreateMap<Domain.Entities.Habit, HabitViewModel>()
            .ForMember(dst => dst.Records, opt => opt.MapFrom(src => src.HabitRecords))
            .ForMember(dst => dst.CountComplete, opt => opt.MapFrom(src => src.HabitRecords != null
                ? src.HabitRecords.Count(r => r.IsComplete)
                : 0));

        CreateMap<AddHabitModel, Domain.Entities.Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore())
            .ForMember(dst => dst.IsOverdue, opt => opt.Ignore());

        CreateMap<UpdateHabitModel, Domain.Entities.Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore());

        CreateMap<HabitRecord, HabitRecordViewModel>();

        CreateMap<HabitRecordViewModel, HabitRecord>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.HabitId, opt => opt.Ignore())
            .ForMember(dst => dst.Habit, opt => opt.Ignore());
    }
}