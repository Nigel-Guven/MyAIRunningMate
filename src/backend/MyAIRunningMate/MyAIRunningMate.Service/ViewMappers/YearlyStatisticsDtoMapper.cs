using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class YearlyStatisticsDtoMapper
{
    public static YearlyStatisticsDto ToYearlyStatisticsDto(this YearlyStatistics entity) => new()
    {
        YearlyRunningDistance =  entity.YearlyRunningDistance,
        YearlySwimmingDistance =  entity.YearlySwimmingDistance,
        YearlyActiveDays =   entity.YearlyActiveDays,
        YearlyAverageTrainingEffect = entity.YearlyAverageTrainingEffect,
        YearlyTotalTrainingEffect = entity.YearlyTotalTrainingEffect
    };
}