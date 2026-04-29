using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAIRunningMate.Domain.Models.Requests;

public class FitFileUploadRequest
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }
}