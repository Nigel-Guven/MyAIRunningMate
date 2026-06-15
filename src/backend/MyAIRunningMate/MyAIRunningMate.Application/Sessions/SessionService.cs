using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Sessions;

public class SessionService(
    SupabaseAuthClient authenticationWrapper,
    IProfileRepository profileRepository)
    : ISessionService
{
    public async Task<SessionResult> LoginAsync(string email, string password)
    {
        var sessionResponse = await authenticationWrapper.Client.Auth.SignIn(email, password);
        
        var user = sessionResponse?.User;
        
        if (sessionResponse?.AccessToken == null || user?.Id == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        
        var userId = Guid.Parse(user.Id);
    
        var profile = await profileRepository.GetByIdAsync(userId);
        if (profile == null)
        {
            throw new InvalidOperationException("Authentication succeeded, but application user profile does not exist.");
        }
        
        return new SessionResult
        {
            Token = sessionResponse.AccessToken,
            UserId = userId
        };
    }

    public async Task LogoutAsync()
    {
        try
        {
            await authenticationWrapper.Client.Auth.SignOut();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while attempting to terminate the session via Supabase.", ex);
        }
    }
}