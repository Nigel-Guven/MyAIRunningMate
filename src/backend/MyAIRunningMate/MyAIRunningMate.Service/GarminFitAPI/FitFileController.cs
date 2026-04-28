using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.Requests;

namespace MyAIRunningMate.Service.GarminFitAPI;

[ApiController]
[Route("api/fitfile")]
public class FitFileController : ControllerBase
{
    private readonly IFitFileService _fitFileService;

    public FitFileController(IFitFileService fitFileService)
    {
        _fitFileService = fitFileService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFitFile([FromForm] FitFileUploadRequest request)
    {
        if (request.File == null || request.File.Length == 0) 
            return BadRequest("File is empty.");

        try 
        {
            var result = await _fitFileService.ProcessAndStoreFitFileAsync(request.File);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal processing error: {ex.Message}");
        }
    }
}