using System.Text.Json.Serialization;

namespace MyAIRunningMate.Client.Python.Responses.Ingestion;

public record PythonSessionResponse(
    [property: JsonPropertyName("session_elapsed_time")] 
    double SessionElapsedTime,
    
    [property: JsonPropertyName("session_moving_time")] 
    double SessionMovingTime,
    
    [property: JsonPropertyName("session_distance_metres")] 
    double SessionDistanceMetres,
    
    [property: JsonPropertyName("session_total_cycles")] 
    int SessionTotalCycles,
    
    [property: JsonPropertyName("session_total_calories")] 
    int SessionTotalCalories,
    
    [property: JsonPropertyName("session_estimated_sweat_loss")] 
    int? SessionEstimatedSweatLoss,
    
    [property: JsonPropertyName("session_average_temperature")] 
    int? SessionAverageTemperature,
    
    [property: JsonPropertyName("session_max_temperature")] 
    int? SessionMaxTemperature,
    
    [property: JsonPropertyName("session_average_heart_rate")] 
    int SessionAverageHeartRate,
    
    [property: JsonPropertyName("session_max_heart_rate")] 
    int SessionMaxHeartRate,
    
    [property: JsonPropertyName("session_average_power")] 
    int? SessionAveragePower,
    
    [property: JsonPropertyName("session_max_power")] 
    int? SessionMaxPower,
    
    [property: JsonPropertyName("session_average_cadence")] 
    int SessionAverageCadence,
    
    [property: JsonPropertyName("session_max_cadence")] 
    int? SessionMaxCadence,
    
    [property: JsonPropertyName("session_average_vertical_oscillation")] 
    double? SessionAverageVerticalOscillation,
    
    [property: JsonPropertyName("session_step_length")] 
    double? SessionStepLength,
    
    [property: JsonPropertyName("session_average_vertical_ratio")] 
    double? SessionAverageVerticalRatio,
    
    [property: JsonPropertyName("session_average_stance_time")] 
    double? SessionAverageStanceTime,
    
    [property: JsonPropertyName("session_aerobic_training_effect")] 
    double SessionAerobicTrainingEffect,
    
    [property: JsonPropertyName("session_anaerobic_training_effect")] 
    double SessionAnaerobicTrainingEffect,
    
    [property: JsonPropertyName("session_average_swolf")] 
    int? SessionAverageSwolf,
    
    [property: JsonPropertyName("session_pool_length")] 
    int? SessionPoolLength
);