using Microsoft.OpenApi;
using MyAIRunningMate.Application.Aggregations;
using MyAIRunningMate.Application.Garmin;
using MyAIRunningMate.Application.Strava;
using MyAIRunningMate.Client;
using MyAIRunningMate.Database.Repository;
using MyAIRunningMate.Domain.Interfaces;
using MyAIRunningMate.Domain.Interfaces.Client;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Repositories.Weight;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Service.StravaAPI;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(StravaController).Assembly);

builder.Services.AddEndpointsApiExplorer();

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:PublicKey"];

builder.Services.AddScoped(_ => 
    new Client(supabaseUrl!, supabaseKey, new SupabaseOptions
    {
        AutoConnectRealtime = true
    }));

builder.Services.AddHttpClient("Strava", client =>
{
    client.BaseAddress = new Uri("https://www.strava.com/api/v3/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IPythonApiClient, PythonApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8000/");
});

builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<ILapRepository, LapRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IStravaResourceMapRepository, StravaResourceMapRepository>();
builder.Services.AddScoped<IStravaResourceRepository, StravaResourceRepository>();
builder.Services.AddScoped<IWeightRepository, WeightRepository>();

builder.Services.AddScoped<IFitFileService, FitFileService>();
builder.Services.AddScoped<IStravaService, StravaService>();
builder.Services.AddScoped<IAggregatorMapper, AggregatorMapper>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

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
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();
app.Run();