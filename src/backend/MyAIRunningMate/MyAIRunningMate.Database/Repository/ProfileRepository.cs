using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class ProfileRepository(Supabase.Client supabase) : BaseRepository<ProfileEntity>(supabase), IProfileRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<ProfileEntity?> GetByIdAsync(Guid userId)
    {
        var response = await _supabase.From<ProfileEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Get();
        
        return response.Models.FirstOrDefault();
    }

    public async Task CreateAsync(ProfileEntity profile)
    {
        await _supabase.From<ProfileEntity>().Insert(profile);
    }
}