using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IFitFileService
{
    Task<ActivityDto> ProcessAndStoreFitFileAsync(IFormFile file);
}