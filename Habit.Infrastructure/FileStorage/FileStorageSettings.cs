namespace Infrastructure.FileStorage;

public class FileStorageSettings
{
    public required string Endpoint { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
}