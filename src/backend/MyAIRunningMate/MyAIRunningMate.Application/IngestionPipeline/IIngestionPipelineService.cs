using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.IngestionPipeline;

public interface IIngestionPipelineService
{
    Task<(Activity activity, int numberOfLaps)> ProcessFitFileAsync(IFormFile file, Guid userId);
}