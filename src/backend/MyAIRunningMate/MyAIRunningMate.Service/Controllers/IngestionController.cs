using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.IngestionPipeline;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Contracts.Ingestion.Requests;
using MyAIRunningMate.Service.Mappers;

namespace MyAIRunningMate.Service.Controllers;

[Authorize]
[ApiController]
[Route("api/ingestion")]
public class IngestionController(IIngestionPipelineService ingestionPipelineService, IUserContext userContext) : ControllerBase
{
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFitFile([FromForm] IngestionRequest request)
    {
        if (request.File.Length == 0) 
            return BadRequest("File is empty.");

        var userId = userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        try 
        {
            var ingestionView = await ingestionPipelineService.ProcessFitFileAsync(request.File, userId);

            var dto = ingestionView.activity.ToResponse(ingestionView.numberOfLaps, ingestionView.status);
            
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal processing error: {ex.Message}");
        }
    }
}