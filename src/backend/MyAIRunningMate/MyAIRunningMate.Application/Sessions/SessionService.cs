using Microsoft.Extensions.Configuration;
using MyAIRunningMate.Application.User;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Session;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IUserContext _userContext;
    private readonly IConfiguration _configuration;
    
    public SessionService(
        ISessionRepository sessionRepository,
        IProfileRepository profileRepository,
        IUserContext userContext,
        IConfiguration configuration)
    {
        _sessionRepository = sessionRepository;
        _profileRepository = profileRepository;
        _userContext = userContext;
        _configuration = configuration;
    }
    
    public async Task<SessionResult> LoginAsync(string email, string password)
    {
        var authClient = await CreateAuthClientAsync();
        var sessionResponse = await authClient.Auth.SignIn(email, password);
        
        if (sessionResponse == null || string.IsNullOrEmpty(sessionResponse.AccessToken))
        {
            throw new InvalidOperationException("Authentication failed or access token is null.");
        }
        
        var userId = Guid.Parse(sessionResponse.User.Id);
        
        var profile = await _profileRepository.GetByIdAsync(userId);
        
        if (profile == null)
        {
            throw new InvalidOperationException("Profile does not exist for the authenticated user.");
        }

        var sessionEntity = await _sessionRepository.GetSessionByUserId(userId);

        bool isStravaConnected = sessionEntity != null && 
            (!string.IsNullOrEmpty(sessionEntity.AccessToken) || 
            !string.IsNullOrEmpty(sessionEntity.RefreshToken));
        
        return new SessionResult()
        {
            Token = sessionResponse.AccessToken,
            UserId = userId,
            IsStravaConnected = isStravaConnected,
        };
    }

    public async Task LogoutAsync()
    {
        var userId = _userContext.GetUserId();
        var session = await _sessionRepository.GetSessionByUserId(userId);

        if (session != null)
        {
            session.AccessToken = null;
            session.RefreshToken = null;
            session.ExpiresAt = null;
            session.UpdatedAt = DateTime.UtcNow;
            await _sessionRepository.SaveSession(session);
        }
    }

    public async Task<bool> HasStravaConnectionAsync(Guid userId)
    {
        var session = await _sessionRepository.GetSessionByUserId(userId);
        
        if (session == null || string.IsNullOrEmpty(session.AccessToken))
            return false;
        
        if (session.ExpiresAt.HasValue)
        {
            var currentTimeSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (currentTimeSeconds >= session.ExpiresAt.Value)
            {
                return false; 
            }
        }

        return true;
    }

    private async Task<Supabase.Client> CreateAuthClientAsync()
    {
        var url = _configuration["Supabase:Url"]
            ?? throw new InvalidOperationException("Supabase URL is not configured.");
        var key =
            _configuration["Supabase:AnonKey"]
            ?? _configuration["Supabase:PublicKey"]
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