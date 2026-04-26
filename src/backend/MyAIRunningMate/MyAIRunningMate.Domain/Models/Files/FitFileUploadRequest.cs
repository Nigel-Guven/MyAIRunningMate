using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAIRunningMate.Domain.Models.Files;

public class FitFileUploadRequest
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }
}