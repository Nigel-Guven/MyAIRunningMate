import type { LapResponse } from "./lapResponse";
import type { TimeSeriesRecordResponse } from "./timeSeriesRecordResponse";


export interface AggregateArtifactResponse {
  activity_id: string;
  garmin_activity_id: string;
  start_time: string;
  exercise_type: string;
  duration_seconds: number;
  moving_time_seconds: number;
  distance_metres: number;
  calories: number;
  average_heart_rate: number | null;
  max_heart_rate: number | null;
  total_elevation_gain: number | null;
  raw_pace_seconds_per_metre: number;
  training_effect: number;
  pool_length: number | null;
  map_polyline: string | null;
  time_series_records: TimeSeriesRecordResponse[];
  laps: LapResponse[];
}