using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public class FileService : IFileService
{
    private readonly GridFSBucket _bucket;
    private readonly MongoGridFsFileStorageOptions _options;

    private const string FilePathMetadataKey = "filePath";
    private const string ContentTypeMetadataKey = "contentType";

    public FileService(GridFSBucket bucket, IOptions<MongoGridFsFileStorageOptions> options)
        => (_bucket, _options) = (bucket, options.Value);

    public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overrite = false, Dictionary<string, string>? tags = null)
    {
        if (file.Length > _options.FileSizeLimitInBytes)
        {
            throw new InvalidOperationException($"File exeeds maximum allowed size of {_options.FileSizeLimitInMB} MB.");
        }

        var metadata = new BsonDocument(tags ?? new Dictionary<string, string>())
        {
            { FilePathMetadataKey, filePath },
            { ContentTypeMetadataKey, file.ContentType }
        };

        var uploadOptions = new GridFSUploadOptions
        {
            Metadata = metadata,
            ChunkSizeBytes = _options.ChunkSizeBytes
        };

        ObjectId fileId;
        using (var stream = file.OpenReadStream())
        {
            fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions);
        }

        return new UploadResponse(
            FilePath: filePath,
            FileName: file.FileName,
            FileSize: file.Length,
            FileId: fileId.ToString()
        );
  
    }

    public Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryDeleteFileAsync(string fileId)
    {
        throw new NotImplementedException();
    }

}
