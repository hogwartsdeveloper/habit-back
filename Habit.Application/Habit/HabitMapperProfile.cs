using AutoMapper;
using Habit.Application.Habit.Models;
using Habit.Core.Entities;

namespace Habit.Application.Habit;

public class HabitMapperProfile : Profile
{
    public HabitMapperProfile()
    {
        CreateMap<Core.Entities.Habit, HabitViewModel>()
            .ForMember(dst => dst.Records, opt => opt.MapFrom(src => src.HabitRecords));

        CreateMap<AddHabitModel, Core.Entities.Habit>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.UserId, opt => opt.Ignore())
            .ForMember(dst => dst.User, opt => opt.Ignore())
            .ForMember(dst => dst.IsOverdue, opt => opt.Ignore());

        CreateMap<UpdateHabitModel, Core.Entities.Habit>()
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