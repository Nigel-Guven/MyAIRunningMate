using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi;
using MyAIRunningMate.Application.Garmin;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Application.UserInterface;
using MyAIRunningMate.Client;
using MyAIRunningMate.Client.Python;
using MyAIRunningMate.Client.Strava;
using MyAIRunningMate.Database.Repository;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Repositories.Weight;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Service.StravaAPI;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Ensure settings exist
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

            // Fetch public key dynamically from Supabase JWKS endpoint for ES256
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

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IStravaApiService, StravaApiService>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<ILinkProviderService, LinkProviderService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IStravaResourceService, StravaResourceService>();

builder.Services.AddScoped<IActivityViewService, ActivityViewService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IIngestionPipelineService, IngestionPipelineService>();

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