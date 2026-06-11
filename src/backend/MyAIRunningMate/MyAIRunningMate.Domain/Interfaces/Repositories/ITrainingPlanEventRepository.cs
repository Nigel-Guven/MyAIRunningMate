using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface ITrainingPlanEventRepository
{
    Task<IEnumerable<TrainingPlanEvent>> GetEventsForUserInDateRangeAsync(Guid trainingPlanId, DateTime start, DateTime end);
    Task<IEnumerable<TrainingPlanEvent>> GetEventsByPlanIdAsync(Guid trainingPlanId);
}