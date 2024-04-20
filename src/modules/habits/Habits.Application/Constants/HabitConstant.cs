namespace Habits.Application.Constants;

public static class HabitConstant
{
    public const string NotFound = "Привычка не найдена!";
    public const string RecordEditExistEntry = "Вы пытаетесь отредактировать существующую запись.";
    public const string RecordDayGreaterToday = "Дата записи не может быть позже сегодняшнего дня.";
    public const string RecordDateInterval = "Дата записей выходит за пределы интервала действия привычки.";
    public const string TitleIsRequired = "Названия обязателен для заполнения.";
    public const string StartDateIsRequired = "Дата начало обязателен для заполнения.";
    public const string EndDateIsRequired = "Дата окончания обязателен для заполнения.";
    public const string RecordDateIsRequired = "Дата обязателен для заполнения.";
}