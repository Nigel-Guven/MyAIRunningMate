using Microsoft.OpenApi.Models;
using MyAIRunningMate.Service.Controllers; 
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(StravaController).Assembly);

var supabaseUrl = builder.Configuration["Supabase:Url"] 
                  ?? throw new InvalidOperationException("Supabase URL is missing.");
var supabaseKey = builder.Configuration["Supabase:Key"] 
                  ?? throw new InvalidOperationException("Supabase Key is missing.");

builder.Services.AddScoped(_ => new Supabase.Client(supabaseUrl, supabaseKey, new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
}));

builder.Services.AddScoped(typeof(IRepository<>), typeof(SupabaseRepository<>));

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAIRunningMate API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Standard React port
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

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();