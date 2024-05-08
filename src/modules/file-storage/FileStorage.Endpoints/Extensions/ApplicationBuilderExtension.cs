using FileStorage.Endpoints.Services;
using Microsoft.AspNetCore.Builder;

namespace FileStorage.Endpoints.Extensions;

/// <summary>
/// Расширение настройки приложения.
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Инициализация модуля fileStorage в приложении.
    /// </summary>
    /// <param name="app">Экземпляр приложения, к которому применяется расширение.</param>
    public static void FileStorageModuleInit(this WebApplication app)
    {
        app.MapGrpcService<FileGrpcService>();
    }
}