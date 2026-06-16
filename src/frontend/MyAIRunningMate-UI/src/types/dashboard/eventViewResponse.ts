export interface EventViewResponse {
  event_name: string;
  event_date: string;
  event_location: string;
  distance_metres: number;
  event_type: string;
  event_url: string | "null";
  event_info: string | "null";
}