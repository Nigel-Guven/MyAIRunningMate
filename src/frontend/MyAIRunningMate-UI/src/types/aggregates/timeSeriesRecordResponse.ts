export interface TimeSeriesRecordResponse {
  timestamp: string;
  distance_metres: number | null;
  heart_rate: number | null;
  cadence: number | null;
  power: number | null;
  latitude: number | null;
  longitude: number | null;
}