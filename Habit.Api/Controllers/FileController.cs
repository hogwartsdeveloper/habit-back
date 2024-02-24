using Habit.Application.FileStorage;
using Habit.Application.FileStorage.Interfaces;
using Habit.Application.FileStorage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FileController(IFileStorageService fileStorageService) : ControllerBase
{
    [HttpGet]
    public Task<FileModel?> GetAsync([FromQuery] string filePath, CancellationToken cancellationToken)
    {
        var fileData = filePath.Split("/");
        var bucketName = fileData[0];
        var fileName = fileData[1];
        
        return fileStorageService.GetAsync(bucketName, fileName, cancellationToken);
    }
}