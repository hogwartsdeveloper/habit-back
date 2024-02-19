using Microsoft.AspNetCore.Http;

namespace Habit.Application.Users.Models;

public class AddUserImageModel
{
    public required IFormFile File { get; set; }
}