namespace MyAIRunningMate.Application.Sessions;

public class SupabaseAuthClient(Supabase.Client client)
{
    public Supabase.Client Client { get; } = client;
}