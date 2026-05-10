namespace MyAIRunningMate.Service;

public static class ApiRoutes
{
    private const string Prefix = "/api";
    
    // Activity
    public const string ActivityViewRoot = Prefix + "/activity";
    public const string ActivityViewAggregate = "aggregate";
    
    // Best Efforts
    public const string BestEffortsRoot = Prefix + "/best_efforts";
    public const string BestEffortsAll = "efforts";
    public const string BestEffortsUpdate = "update";
    
    // Calendar
    public const string CalendarRoot = Prefix + "/calendar";
    public const string CalendarDisplay = "display";
    
    // Events
    public const string EventsRoot = Prefix + "/events";
    public const string EventsUpcoming = "upcoming";
    public const string EventsPrimary = "single";
    
    // Ingestion
    public const string IngestionRoot = Prefix + "/fitfile";
    public const string IngestionFileUpload = "upload";
    
    // Session
    public const string SessionRoot = Prefix + "/session";
    public const string SessionLogin = "login";
    public const string SessionLogout = "logout";
    
    // Strava API
    public const string StravaRoot = Prefix + "/strava";
    public const string StravaConnect = "connect";
    public const string StravaCallback = "callback";
    public const string StravaStatus = "status";
    
    // Weight
    public const string WeightRoot = Prefix + "/weight";
    public const string WeightLatest = "latest";
    public const string WeightHistory = "history";
    public const string WeightLog = "log_weight";
}