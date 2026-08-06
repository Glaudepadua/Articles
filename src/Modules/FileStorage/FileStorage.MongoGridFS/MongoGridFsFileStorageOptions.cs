namespace FileStorage.MongoGridFS;

public class MongoGridFsFileStorageOptions
{
    public string DatabaseName { get; init; } = default!;

    public string BucketName { get; init; } = "files";

    public int ChunkSizeBytes { get; init; } = 1048576; // 1MB

    public long FileSizeLimitInMB { get; init; } = 50;

    public long FileSizeLimitInBytes => FileSizeLimitInMB * 1024 * 1024;
}
