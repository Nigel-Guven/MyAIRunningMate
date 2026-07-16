export interface  UserMetricsResponse {
    last_recorded_time: string;
    age: number;
    weight_kg: number;
    user_volumetric_oxygen_max: number;
    user_max_heart_rate: number;
    user_lactate_threshold_heart_rate: number;
    user_lactate_threshold_power: number;
    user_lactate_threshold_speed: number;
    user_volumetric_oxygen_max_rating: string;
    fitness_percentile: number;
    power_to_weight_ratio: number;
    power_rating: string;
    threshold_percentage_power: number;
    training_level: string;
    fitness_rank_color: string;
}