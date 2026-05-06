using MyAIRunningMate.Contracts.Login.Responses;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Application.Strava;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IUserContext _userContext;
    private readonly Supabase.Client _supabaseClient;
    
    public SessionService(
        ISessionRepository sessionRepository,
        IProfileRepository profileRepository,
        IUserContext userContext,
        Supabase.Client supabaseClient)
    {
        _sessionRepository = sessionRepository;
        _profileRepository = profileRepository;
        _userContext = userContext;
        _supabaseClient = supabaseClient;
    }
    
    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        var sessionResponse = await _supabaseClient.Auth.SignIn(email, password);
        
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
        
        return new LoginResponse
        {
            Token = sessionResponse.AccessToken,
            UserId = userId,
            IsStravaConnected = isStravaConnected
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
        return session != null && !string.IsNullOrEmpty(session.AccessToken);
    }
}