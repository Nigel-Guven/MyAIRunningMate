export interface ActivityDetailsResponse {
  activity_id: number;
  garmin_activity_id: string;
  start_time: string;
  total_time: number;
  moving_time: number;
  distance_metres: number;
  beginning_body_battery: number;
  beginning_body_potential: number;
  ending_body_battery: number;
  ending_potential: number;
  total_ascent: number | null;
  total_descent: number | null;
  recovery_time: number;
  exercise_type: string;
  exercise_subtype: string;
  exercise_name: string;
  user_volumetric_oxygen_max: number;
  user_max_heart_rate: number;
  user_lactate_threshold_heart_rate: number;
  user_lactate_threshold_power: number;
  user_lactate_threshold_speed: number;
  number_of_laps: number;
  location: string | null;
  map_polyline: string | null;
}