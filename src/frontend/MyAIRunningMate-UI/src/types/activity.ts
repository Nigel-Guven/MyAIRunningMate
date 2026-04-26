// src/types/activity.ts
export interface ActivityResult {
  id: number;
  garmin_id: string;
  garmin_activity_id: string;
  start_time: string;
  type: string;
  duration_seconds: number;
  distance_metres: number;
  avg_heart_rate: number | null;
  max_heart_rate: number | null;
  total_elevation_gain: number | null;
  avg_seconds_per_km: number;
  training_effect: number;
  created_at: string;
  strava_resource_id: string;
}