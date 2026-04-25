using Microsoft.AspNetCore.Http;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IFitFileParserService
{
    Task<ActivityDto> ProcessFile(IFormFile file);
}