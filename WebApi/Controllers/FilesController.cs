using System.Net;
using Domain.FileStorage;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ELogBook.Controllers;

[ApiController]
[Authorize]
[Route("api/" + "[controller]")]
public class FilesController(IFileStorageService fileStorage) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ActionResult<string, ErrorInfo>> Upload(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileId = await fileStorage.UploadFileAsync(
            stream, 
            file.FileName, 
            file.ContentType);

        return Ok(new { FileId = fileId.ToString() });
    }

    [HttpGet("{fileId}")]
    public async Task<ActionResult<FileContentResult, ErrorInfo>> Download(string fileId)
    {
        var fileObjectId = new ObjectId(fileId);
        var metadata = await fileStorage.GetFileInfoAsync(fileObjectId);
        if (metadata is null)
            return new ErrorInfo("File not found", $"Couldn't find file with id {fileId}", HttpStatusCode.NotFound);
        
        var bytes = await fileStorage.DownloadFileAsync(new ObjectId(fileId));
        return File(bytes, metadata.ContentType);
    }
}