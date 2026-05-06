namespace MyAIRunningMate.Application.Extensions;

public static class GuidEx
{
    public static bool IsGuid(Guid? value)
    {
        return value != Guid.Empty;
    }
}