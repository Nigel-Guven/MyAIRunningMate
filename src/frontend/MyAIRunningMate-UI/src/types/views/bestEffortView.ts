export interface BestEffortViewDto {
  distance_metres: number;
  distance_label: string;
  time_seconds: number | null;
  achieved_at: string | "null";
}