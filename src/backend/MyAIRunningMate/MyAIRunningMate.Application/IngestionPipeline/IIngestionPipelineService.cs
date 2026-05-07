using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Application.IngestionPipeline;

public interface IIngestionPipelineService
{
    Task<IngestionView> ProcessFitFileAsync(IFormFile file, Guid userId);
}