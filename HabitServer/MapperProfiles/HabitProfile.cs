using AutoMapper;
using HabitServer.Entities;
using HabitServer.Models.Habits;

namespace HabitServer.MapperProfiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Habit, HabitViewModel>();
    }
}