import type { LapDto } from "./lap";
import type { MapDto } from "./map";

export interface AggregateArtifactDto {
  activity_id: string;
  resource_id: string;
  garmin_activity_id: string;
  strava_id: string | null;
  name: string;
  exercise_type: string;
  start_time: string;
  elapsed_time: number;
  average_cadence: number | null;
  average_second_per_kilometre: number;
  total_elevation_gain: number | null;
  elevation_low: number;
  elevation_high: number;
  duration_seconds: number;
  distance_metres: number;
  average_heart_rate: number;
  max_heart_rate: number;
  training_effect: number;
  achievement_count: number;
  kudos_count: number;
  athlete_count: number;
  personal_record_count: number;
  map: MapDto | null;
  laps: LapDto[];
}