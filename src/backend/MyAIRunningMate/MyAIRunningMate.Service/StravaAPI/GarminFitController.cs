using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Service.StravaAPI;

[ApiController]
[Route("api/[controller]")]
public class GarminFitController : ControllerBase
{
    private readonly IFitFileParserService _fitFileParserService;
    
    public GarminFitController(IFitFileParserService fitFileParserService)
    {
        _fitFileParserService = fitFileParserService;
    }
    
    [HttpPut("upload-fit-file")]
    public async Task<IActionResult> UploadFitFile(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var activities = await _fitFileParserService.ProcessFile(file);

        return Ok(activities);
    }
}