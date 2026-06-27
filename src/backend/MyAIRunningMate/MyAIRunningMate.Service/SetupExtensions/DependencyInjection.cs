using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.Aggregate;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Application.Events;
using MyAIRunningMate.Application.IngestionPipeline;
using MyAIRunningMate.Application.Insights;
using MyAIRunningMate.Application.Sessions;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Application.Weights;
using MyAIRunningMate.Client.Geocoder;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Database.Repository;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using Supabase;

namespace MyAIRunningMate.Service.SetupExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<ILapRepository, LapRepository>();
        services.AddScoped<IWeightRepository, WeightRepository>();
        services.AddScoped<IBestEffortsRepository, BestEffortsRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();
        services.AddScoped<ITrainingPlanEventRepository, TrainingPlanEventRepository>();
        services.AddScoped<ITimeSeriesRecordRepository, TimeSeriesRecordRepository>();
        
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ISessionService, SessionService>();

        services.AddScoped<IActivityService, ActivityService>();
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
            if (!string.IsNullOrEmpty(pythonApiBaseUrl)) 
                client.BaseAddress = new Uri(pythonApiBaseUrl);
        });
    
        services.AddHttpClient<IGeocodeClient, GeocodeClient>(client =>
        {
            var geocodeUrl = configuration["Geocoding:BaseUrl"];
            if (!string.IsNullOrEmpty(geocodeUrl)) 
                client.BaseAddress = new Uri(geocodeUrl);
        });

        return services;
    }

    public static IServiceCollection AddSupabaseAuthClient(this IServiceCollection services, string url, string anonKey)
    {
        services.AddScoped<Supabase.Client>(provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            
            string? token = null;
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authHeader["Bearer ".Length..].Trim();
            }

            var options = new SupabaseOptions
            {
                AutoRefreshToken = false, 
                AutoConnectRealtime = false
            };

            if (!string.IsNullOrEmpty(token))
            {
                options.Headers ??= new Dictionary<string, string>();
                options.Headers["Authorization"] = $"Bearer {token}";
            }

            return new Supabase.Client(url, anonKey, options);
        });
        
        services.AddScoped<SupabaseAuthClient>(provider =>
        {
            var baseClient = provider.GetRequiredService<Supabase.Client>();
            return new SupabaseAuthClient(baseClient);
        });

        return services;
    }
}