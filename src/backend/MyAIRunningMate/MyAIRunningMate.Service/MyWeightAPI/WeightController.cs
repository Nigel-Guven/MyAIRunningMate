using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Application.Weight;
using MyAIRunningMate.Contracts.Weight;
using MyAIRunningMate.Service.ViewMappers;

namespace MyAIRunningMate.Service.MyWeightAPI;

[Authorize]
[ApiController]
[Route(ApiRoutes.WeightRoot)]
public class WeightController : ControllerBase
{
    private readonly IWeightService _weightService;
    private readonly IUserContext _userContext;

    public WeightController(IWeightService weightService,  IUserContext userContext)
    {
        _weightService = weightService;
        _userContext =  userContext;
    }
    
    [HttpGet(ApiRoutes.WeightLatest)]
    public async Task<IActionResult> GetLatestSingleWeight()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var result = await _weightService.GetLatestWeightAsync(userId);

        var dto = result.ToWeightViewDto();
        return Ok(dto);
    }

    [HttpGet(ApiRoutes.WeightHistory)]
    public async Task<IActionResult> GetLatestMultipleWeights()
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var weightEntities = await _weightService.GetWeightHistoryAsync(userId);
        
        var dtos = weightEntities.Select(entity => entity.ToWeightViewDto()).ToList();
        
        return Ok(dtos);
    }
    
    [HttpPost(ApiRoutes.WeightLog)]
    public async Task<IActionResult> LogWeight([FromBody] WeightRequest request)
    {
        var userId = _userContext.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try 
        {
            await _weightService.LogWeightAsync(request.WeightInPounds, userId);
            
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database error: {ex.Message}");
        }
    }
}