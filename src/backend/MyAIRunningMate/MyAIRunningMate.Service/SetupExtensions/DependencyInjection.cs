using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Application.Events;
using MyAIRunningMate.Application.IngestionPipeline;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.LinkProvider;
using MyAIRunningMate.Application.Sessions;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Application.Weights;
using MyAIRunningMate.Client.Geocoder;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Database.Repository;
using MyAIRunningMate.Domain.Interfaces.Repositories;

namespace MyAIRunningMate.Service.SetupExtensions;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure()
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

        public IServiceCollection AddApplicationServices()
        {
            services.AddScoped<IUserContext, UserContext>();
        
            services.AddScoped<ISessionService, SessionService>();
        
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

        public IServiceCollection AddHttpClients(IConfiguration configuration)
        {
            services.AddHttpClient<IPythonApiClient, PythonApiClient>(client =>
            {
                var pythonApiBaseUrl = configuration["PythonApi:BaseUrl"];
                if (pythonApiBaseUrl != null) client.BaseAddress = new Uri(pythonApiBaseUrl);
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
}