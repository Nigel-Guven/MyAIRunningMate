import { http } from './http';
import { API_ENDPOINTS } from './endpoints';

export interface StravaConnectResponse {
  url: string;
}

export interface StravaStatusResponse {
  isStravaConnected: boolean;
}

export const stravaApi = {
  getConnectUrl: () =>
    http.get<StravaConnectResponse>(
      API_ENDPOINTS.strava.connect
    ),

  getStatus: () =>
    http.get<StravaStatusResponse>(
      API_ENDPOINTS.strava.status
    ),
};