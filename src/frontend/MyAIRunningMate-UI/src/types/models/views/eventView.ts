export interface EventViewDto {
  name: string;
  event_date: string;
  location: string;
  distance_metres: number;
  event_type: string;
  event_url: string | "null";
  event_info: string | "null";
}