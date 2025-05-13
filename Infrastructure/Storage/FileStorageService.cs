using Domain.FileStorage;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using FileInfo = Domain.FileStorage.FileInfo;

namespace Infrastructure.Storage;

public class FileStorageService(IMongoDatabase database) : IFileStorageService
{
    private readonly GridFSBucket _gridFsBucket = new GridFSBucket(database);

    public async Task<ObjectId> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument
            {
                { "contentType", contentType },
                { "uploadDate", DateTime.UtcNow }
            }
        };

        return await _gridFsBucket.UploadFromStreamAsync(fileName, fileStream, options);
    }

    public async Task<byte[]> DownloadFileAsync(ObjectId fileId)
    {
        return await _gridFsBucket.DownloadAsBytesAsync(fileId);
    }

    public async Task DeleteFileAsync(ObjectId fileId)
    {
        await _gridFsBucket.DeleteAsync(fileId);
    }

    public async Task<FileInfo?> GetFileInfoAsync(ObjectId fileId)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", fileId);
        var fileInfo = await (await _gridFsBucket.FindAsync(filter)).FirstOrDefaultAsync();

        if (fileInfo == null)
            return null;

        return new FileInfo
        {
            Id = fileInfo.Id.ToString(),
            FileName = fileInfo.Filename,
            ContentType = fileInfo.Metadata?.GetValue("contentType", "").AsString,
            Length = fileInfo.Length,
            UploadDate = fileInfo.UploadDateTime
        };
    }
}