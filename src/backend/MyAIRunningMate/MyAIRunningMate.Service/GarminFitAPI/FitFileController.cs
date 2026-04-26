using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;

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
    public async Task<IActionResult> UploadFitFile(IFormFile? file)
    {
        if (file == null || file.Length == 0) return BadRequest("File is empty.");

        try 
        {
            var result = await _fitFileService.ProcessAndStoreFitFileAsync(file);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal processing error: {ex.Message}");
        }
    }
}