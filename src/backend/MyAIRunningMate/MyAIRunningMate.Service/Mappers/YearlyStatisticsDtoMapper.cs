using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Contracts.Analytics.Responses;

namespace MyAIRunningMate.Service.Mappers;

public static class YearlyStatisticsDtoMapper
{
    public static YearlyStatisticsResponse ToYearlyStatisticsDto(this YearlyStatistics model) => new()
    {
        YearlyRunningDistance =  model.YearlyRunningDistance,
        YearlySwimmingDistance =  model.YearlySwimmingDistance,
        YearlyActiveDays =   model.YearlyActiveDays,
        YearlyAverageTrainingEffect = model.YearlyAverageTrainingEffect,
        YearlyTotalTrainingEffect = model.YearlyTotalTrainingEffect
    };
}