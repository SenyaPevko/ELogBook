namespace Domain.FileStorage;

public class FileInfo
{
    public string Id { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long Length { get; set; }
    public DateTime UploadDate { get; set; }
}