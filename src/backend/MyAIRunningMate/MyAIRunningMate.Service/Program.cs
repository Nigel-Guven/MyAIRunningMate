using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi;
using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.AggregatePage;
using MyAIRunningMate.Application.BestEfforts;
using MyAIRunningMate.Application.Calendar;
using MyAIRunningMate.Application.Events;
using MyAIRunningMate.Application.IngestionPipeline;
using MyAIRunningMate.Application.LinkProvider;
using MyAIRunningMate.Application.Session;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.TrainingPlans;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Application.Weight;
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
using MyAIRunningMate.Service.StravaAPI;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:PublicKey"];

if (string.IsNullOrEmpty(supabaseUrl))
{
    throw new InvalidOperationException("Supabase URL is missing from appsettings.json.");
}

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = $"{supabaseUrl}/auth/v1";
        options.Audience = "authenticated";

        var jwksUrl = $"{supabaseUrl}/auth/v1/.well-known/jwks.json";
        var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            jwksUrl,
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever()
        );

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{supabaseUrl}/auth/v1",

            ValidateAudience = true,
            ValidAudience = "authenticated",

            ValidateLifetime = true,
            
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var config = configurationManager.GetConfigurationAsync().GetAwaiter().GetResult();
                return config.SigningKeys;
            }
        };
    });

builder.Services.AddControllers()
    .AddApplicationPart(typeof(StravaController).Assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton(_ => 
{
    var options = new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    };

    var client = new Client(supabaseUrl!, supabaseKey!, options);
    client.InitializeAsync().GetAwaiter().GetResult();
    return client;
});

builder.Services.AddHttpClient<IStravaApiClient, StravaApiClient>(client =>
{
    client.BaseAddress = new Uri("https://www.strava.com/");
});

builder.Services.AddHttpClient<IPythonApiClient, PythonApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8000/");
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<ILapRepository, LapRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IStravaResourceMapRepository, StravaResourceMapRepository>();
builder.Services.AddScoped<IStravaResourceRepository, StravaResourceRepository>();
builder.Services.AddScoped<IWeightRepository, WeightRepository>();
builder.Services.AddScoped<IBestEffortsRepository, BestEffortsRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();
builder.Services.AddScoped<ITrainingPlanEventRepository, TrainingPlanEventRepository>();

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IStravaApiService, StravaApiService>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<ILinkProviderService, LinkProviderService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IStravaResourceService, StravaResourceService>();
builder.Services.AddScoped<IWeightService, WeightService>();
builder.Services.AddScoped<IBestEffortService, BestEffortService>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IActivityViewService, ActivityViewService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IIngestionPipelineService, IngestionPipelineService>();
builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAIRunningMate API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAIRunningMate v1"));
}
//else
//{ 
//    app.UseHttpsRedirection();
//}

app.UseRouting();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();