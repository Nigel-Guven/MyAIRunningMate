using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MyAIRunningMate.Service.SetupExtensions;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

var supabaseUrl = builder.Configuration["Supabase:Url"] 
                  ?? throw new InvalidOperationException("Supabase URL is missing from appsettings.json.");
var supabaseServiceRoleKey = builder.Configuration["Supabase:ServiceRoleKey"]
                             ?? builder.Configuration["Supabase:PublicKey"]
                             ?? throw new InvalidOperationException("Supabase ServiceRoleKey/PublicKey is missing. Backend database access requires it.");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = $"{supabaseUrl}/auth/v1";
        options.Audience = "authenticated";
        options.MetadataAddress = $"{supabaseUrl}/auth/v1/.well-known/jwks.json";
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{supabaseUrl}/auth/v1",
            ValidateAudience = true,
            ValidAudience = "authenticated",
            ValidateLifetime = true
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton(_ =>
{
    var options = new SupabaseOptions
    {
        AutoRefreshToken = false,
        AutoConnectRealtime = false,
    };

    return new Client(supabaseUrl, supabaseServiceRoleKey, options);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAIRunningMate Dotnet API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Frontend:BaseUrl").Get<string[]>();
        if (allowedOrigins is { Length: > 0 })
        {
            policy.WithOrigins(allowedOrigins);
        }
        else if (builder.Environment.IsDevelopment())
        {
            policy.WithOrigins("http://localhost:5173");
        }
        policy.AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAIRunningMate v1"));
}

app.UseRouting();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();