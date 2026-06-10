using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Application.Events;
using MyAIRunningMate.Application.IngestionPipeline;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.LinkProvider;
using MyAIRunningMate.Application.Session;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Application.Weight;
using MyAIRunningMate.Client.Geocoder;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Client.Strava;
using MyAIRunningMate.Database.Repository;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Interfaces.Repositories.BestEfforts;
using MyAIRunningMate.Domain.Interfaces.Repositories.Events;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Repositories.TrainingPlan;
using MyAIRunningMate.Domain.Interfaces.Repositories.Weight;

namespace MyAIRunningMate.Service.SetupExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<ILapRepository, LapRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IStravaResourceMapRepository, StravaResourceMapRepository>();
        services.AddScoped<IStravaResourceRepository, StravaResourceRepository>();
        services.AddScoped<IWeightRepository, WeightRepository>();
        services.AddScoped<IBestEffortsRepository, BestEffortsRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();
        services.AddScoped<ITrainingPlanEventRepository, TrainingPlanEventRepository>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IStravaApiService, StravaApiService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ILinkProviderService, LinkProviderService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IStravaResourceService, StravaResourceService>();
        services.AddScoped<IWeightService, WeightService>();
        services.AddScoped<IBestEffortService, BestEffortService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IInsightsService, InsightsService>();
        
        services.AddScoped<IActivityViewService, ActivityViewService>();
        services.AddScoped<ICalendarService, CalendarService>();
        services.AddScoped<IIngestionPipelineService, IngestionPipelineService>();
        services.AddScoped<ITrainingPlanService, TrainingPlanService>();

        return services;
    }
    
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IPythonApiClient, PythonApiClient>(client =>
        {
            var pythonApiBaseUrl = configuration["PythonApi:BaseUrl"];
            if (pythonApiBaseUrl != null) client.BaseAddress = new Uri(pythonApiBaseUrl);
        });
        
        services.AddHttpClient<IStravaApiClient, StravaApiClient>(client =>
        {
            var stravaUrl = configuration["Strava:BaseUrl"];
            if (stravaUrl != null) client.BaseAddress = new Uri(stravaUrl);
        });
        
        services.AddHttpClient<IGeocodeClient, GeocodeClient>(client =>
        {
            var geocodeUrl = configuration["Geocoding:BaseUrl"];
            if (geocodeUrl != null) client.BaseAddress = new Uri(geocodeUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "MyAIRunningMateApp/1.0");
        });

        return services;
    }
}