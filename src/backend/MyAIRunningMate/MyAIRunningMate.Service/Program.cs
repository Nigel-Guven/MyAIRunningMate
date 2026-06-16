using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyAIRunningMate.Service.SetupExtensions;

var builder = WebApplication.CreateBuilder(args);

var supabaseUrl = builder.Configuration["Supabase:Url"] 
                  ?? throw new InvalidOperationException("Supabase URL is missing.");

var supabaseAnonKey = builder.Configuration["Supabase:PublicKey"] 
                      ?? builder.Configuration["Supabase:AnonKey"]
                      ?? throw new InvalidOperationException("Supabase AnonKey/PublicKey is missing.");

var issuer = $"{supabaseUrl}/auth/v1";

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.MetadataAddress = $"{supabaseUrl}/auth/v1/.well-known/openid-configuration";
        options.Authority = issuer;
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = "authenticated",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            NameClaimType = "sub",
            RoleClaimType = "role"
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplicationServices();

builder.Services.AddSupabaseAuthClient(supabaseUrl, supabaseAnonKey);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();