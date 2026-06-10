using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi;
using MyAIRunningMate.Service.SetupExtensions;
using MyAIRunningMate.Service.StravaAPI;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseServiceRoleKey =
    builder.Configuration["Supabase:ServiceRoleKey"]
    ?? builder.Configuration["Supabase:PublicKey"];

if (string.IsNullOrEmpty(supabaseUrl))
{
    throw new InvalidOperationException("Supabase URL is missing from appsettings.json.");
}

if (string.IsNullOrEmpty(supabaseServiceRoleKey))
{
    throw new InvalidOperationException(
        "Supabase ServiceRoleKey is missing. Backend database access requires the service role key.");
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
        AutoRefreshToken = false,
        AutoConnectRealtime = false,
    };

    var client = new Client(supabaseUrl!, supabaseServiceRoleKey!, options);
    client.InitializeAsync().GetAwaiter().GetResult();
    return client;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAIRunningMate API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        if (allowedOrigins is { Length: > 0 })
        {
            policy.WithOrigins(allowedOrigins);
        }
        else
        {
            policy.WithOrigins("http://localhost:5173");
        }

        policy.AllowAnyHeader()
            .AllowAnyMethod();
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