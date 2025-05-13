using MongoDB.Bson;

namespace Domain.FileStorage;

public interface IFileStorageService
{
    Task<ObjectId> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<byte[]> DownloadFileAsync(ObjectId fileId);
    Task DeleteFileAsync(ObjectId fileId);
    Task<FileInfo?> GetFileInfoAsync(ObjectId fileId);
}