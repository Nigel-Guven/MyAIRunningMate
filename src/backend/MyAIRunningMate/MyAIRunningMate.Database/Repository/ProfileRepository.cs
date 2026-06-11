using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers; 
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class ProfileRepository(Supabase.Client supabase) : BaseRepository<ProfileEntity>(supabase), IProfileRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<Profile?> GetByIdAsync(Guid userId)
    {
        var response = await _supabase.From<ProfileEntity>()
            .Filter("user_id", Constants.Operator.Equals, userId.ToString())
            .Get();

        return response.Models.FirstOrDefault()?.ToDomain();
    }

    public async Task CreateAsync(Profile profile)
    {
        ProfileEntity entityToInsert = profile.ToEntity();

        await _supabase.From<ProfileEntity>().Insert(entityToInsert);
    }
}