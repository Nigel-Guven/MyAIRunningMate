using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Session;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Application.Strava;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserContext _userContext;
    
    public SessionService(ISessionRepository sessionRepository, IUserContext context)
    {
        _sessionRepository = sessionRepository;
        _userContext = context;
    }
    
    public async Task<bool> LoginAsync(string email, string password)
    {
        var tokenString = _userContext.GenerateJwtToken(email);
        var userId = _userContext.GetUserId();

        var session = new SessionEntity
        {
            UserId = userId,
            AccessToken = tokenString,
            UpdatedAt = DateTime.UtcNow
        };

        await _sessionRepository.SaveSession(session);
        return true;
    }
    
    public async Task LogoutAsync()
    {
        var userId = _userContext.GetUserId();
        var session = await _sessionRepository.GetSessionByUserId(userId);

        if (session != null)
        {
            session.AccessToken = null;
            session.RefreshToken = null;
            session.UpdatedAt = DateTime.UtcNow;
            
            await _sessionRepository.SaveSession(session);
        }
    }
}