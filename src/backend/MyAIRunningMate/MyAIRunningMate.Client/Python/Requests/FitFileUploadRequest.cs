using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAIRunningMate.Client.Python.Requests;

public class FitFileUploadRequest
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }
}