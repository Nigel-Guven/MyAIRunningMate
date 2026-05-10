using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.IngestionPipeline;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Client.Python.Requests;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.IngestionAPI;

[Authorize]
[ApiController]
[Route(ApiRoutes.IngestionRoot)]
public class IngestionController : ControllerBase
{
    private readonly IIngestionPipelineService _ingestionPipelineService;
    private readonly IUserContext _userContext;

    public IngestionController(IIngestionPipelineService ingestionPipelineService,  IUserContext userContext)
    {
        _ingestionPipelineService = ingestionPipelineService;
        _userContext = userContext;
    }

    [HttpPost(ApiRoutes.IngestionFileUpload)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFitFile([FromForm] FitFileUploadRequest request)
    {
        if (request.File.Length == 0) 
            return BadRequest("File is empty.");

        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        try 
        {
            var ingestionView = await _ingestionPipelineService.ProcessFitFileAsync(request.File, userId);

            var dto = ingestionView.ToIngestionViewDto();
            
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal processing error: {ex.Message}");
        }
    }
}