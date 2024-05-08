using FileStorage.Application.FileStorage.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace FileStorage.Endpoints.Services;

/// <summary>
/// Сервис для загрузки файлов через gRPC.
/// </summary>
public class FileGrpcService(IFileStorageService storageService) : FileStorageGrpcService.FileStorageGrpcServiceBase
{
    /// <summary>
    /// Асинхронный метод для загрузки файла, получаемого через поток gRPC.
    /// Метод принимает поток запросов, содержащий метаданные и фрагменты данных файла,
    /// и сохраняет файл с использованием предоставленного сервиса хранения файлов.
    /// </summary>
    /// <param name="requestStream">Поток запросов, содержащий метаданные файла и его содержимое.</param>
    /// <param name="context">Контекст вызова gRPC, содержащий информацию о текущем вызове метода.</param>
    /// <returns>Возвращает объект Empty в случае успешной загрузки файла.</returns>
    /// <exception cref="RpcException">Генерирует исключение, если входные данные неполные
    /// или произошла внутренняя ошибка при обработке файла.</exception>
    public override async Task<Empty> Upload(
        IAsyncStreamReader<FileUploadRequest> requestStream,
        ServerCallContext context)
    {
        if (!await requestStream.MoveNext())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "No input received"));
        }
        
        var initialMessage = requestStream.Current;

        if (string.IsNullOrEmpty(initialMessage.BucketName) ||
            string.IsNullOrEmpty(initialMessage.FileName) ||
            string.IsNullOrEmpty(initialMessage.ContentType))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "BucketName or FileName or ContentType cannot be empty"));
        }

        using var memoryStream = new MemoryStream();
        do
        {
            await memoryStream.WriteAsync(requestStream.Current.Content.ToArray());
        } while (await requestStream.MoveNext());

        try
        {
            await storageService.UploadAsync(
                initialMessage.BucketName,
                initialMessage.FileName,
                initialMessage.ContentType,
                initialMessage.Length,
                memoryStream);
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
        
        return new Empty();
    }
}