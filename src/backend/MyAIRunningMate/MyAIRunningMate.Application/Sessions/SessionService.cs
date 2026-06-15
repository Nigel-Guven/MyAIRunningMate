using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Sessions;

public class SessionService(
    ISessionRepository sessionRepository,
    IProfileRepository profileRepository,
    IUserContext userContext,
    IConfiguration configuration)
    : ISessionService
{
    public async Task<SessionResult> LoginAsync(string email, string password)
    {
        var authClient = await CreateAuthClientAsync();
        var sessionResponse = await authClient.Auth.SignIn(email, password);
        
        if (sessionResponse == null || string.IsNullOrEmpty(sessionResponse.AccessToken))
        {
            throw new InvalidOperationException("Authentication failed or access token is null.");
        }
        
        var userId = Guid.Parse(sessionResponse.User.Id);
        
        var profile = await profileRepository.GetByIdAsync(userId);
        
        if (profile == null)
        {
            throw new InvalidOperationException("Profile does not exist for the authenticated user.");
        }
        
        return new SessionResult()
        {
            Token = sessionResponse.AccessToken,
            UserId = userId
        };
    }

    public async Task LogoutAsync()
    {
        var userId = userContext.GetUserId();
        var session = await sessionRepository.GetSessionByUserId(userId);
    }

    private async Task<Supabase.Client> CreateAuthClientAsync()
    {
        var url = configuration["Supabase:Url"]
            ?? throw new InvalidOperationException("Supabase URL is not configured.");
        var key =
            configuration["Supabase:AnonKey"]
            ?? configuration["Supabase:PublicKey"]
            ?? throw new InvalidOperationException(
                "Supabase AnonKey (or PublicKey) is required for user sign-in.");

        var options = new Supabase.SupabaseOptions
        {
            AutoRefreshToken = false,
            AutoConnectRealtime = false,
        };

        var client = new Supabase.Client(url, key, options);
        await client.InitializeAsync();
        return client;
    }
}